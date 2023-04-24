using Cinemachine;
using TMPro;
using TreeEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

[RequireComponent(typeof(Animator)), RequireComponent(typeof(PlayerInput)), RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    // Set on awake
    private Animator _animator; 
    private PlayerInput _playerInput;
    private CharacterController _characterController;
    private Camera _camera;

    // Set in inspector
    [SerializeField]
    private CinemachineVirtualCamera _virtualCameraThatOverrides;
    [SerializeField]
    private LayerMask _aimingLayerMask = new LayerMask();

    private Vector2 _direction;
    private bool _isMoving;
    private bool _isSprinting;
    private bool _isAiming;


    private float _speed;
    private Vector2 _playerVelocity;
    private Vector2 _smoothedPlayerVelocity;
    private Vector3 _aimTarget;


    public float movementSpeed = 1f;
    public float sprintSpeed = 3f;
    public float smoothTime = 1f;
    public float rotationSpeed = 10f;
    public bool isCursorLocked = true;

    // Start is called before the first frame update
    void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerInput = GetComponent<PlayerInput>();
        _characterController = GetComponent<CharacterController>();
        _camera = Camera.main;
    }

  
    public void OnMove(InputAction.CallbackContext value)
    {
        _direction = value.ReadValue<Vector2>();
 
        if(_direction == Vector2.zero)
        {
            _isMoving = false;
        }
        else
        {
            _isMoving = true;
        }
    }


    public void OnSprint(InputAction.CallbackContext value)
    {
        //_isSprinting = value.ReadValueAsButton();

        if(value.started)
        {
            _isSprinting = true;

        }
        else if(value.canceled)
        {
            _isSprinting = false;

        }
    }

    public void OnAim(InputAction.CallbackContext value)
    {
        _isAiming = value.ReadValueAsButton();
    }

    private void Start()
    {
        SetCursorState(isCursorLocked);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        _speed = _isSprinting ? sprintSpeed : !_isMoving ? 0 : movementSpeed;


        _playerVelocity = Vector2.SmoothDamp(_playerVelocity, _direction, ref _smoothedPlayerVelocity, 1);

        Vector3 move = new Vector3(_playerVelocity.x, 0, _playerVelocity.y);
        move = move.x * _camera.transform.right.normalized + move.z * _camera.transform.forward.normalized;
        move.y = 0;
        _characterController.Move(move.normalized * Time.fixedDeltaTime * _speed);

        Vector2 myScreenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = _camera.ScreenPointToRay(myScreenCenter);
        if(Physics.Raycast(ray, out RaycastHit raycastHit, 200, _aimingLayerMask))
        {
            _aimTarget = raycastHit.point;
        }
        else
        {
            _aimTarget = ray.GetPoint(200);
        }

        _aimTarget.y = transform.position.y;
        Vector3 aimDirection = (_aimTarget - transform.position).normalized;

        if(move != Vector3.zero && !_isAiming)
        {
            transform.forward = move;
            _playerVelocity.y = 0;
            _playerVelocity.x = 0;
        }
        else if (_isAiming)
        {
            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.fixedDeltaTime * 20);
        }

        transform.forward = move;
        Quaternion rotation = Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.fixedDeltaTime * rotationSpeed);
    }

    private void Update()
    {

        if (!_isAiming)
        {
            CasualMovement(Time.fixedDeltaTime);
            SetAnimationActiveLayer(_animator, 1, 0, Time.fixedDeltaTime, 10);
            SetAnimationActiveLayer(_animator, 2, 0, Time.fixedDeltaTime, 10);
            _virtualCameraThatOverrides.Priority = 9;
        }
        else
        {
            SetAnimationActiveLayer(_animator, 1, 1, Time.fixedDeltaTime, 10);
            SetAnimationActiveLayer(_animator, 2, 1, Time.fixedDeltaTime, 10);
            _virtualCameraThatOverrides.Priority = 11;


        }

    }
    private void LateUpdate()
    {
        _animator.SetFloat("ForwardMotion", _playerVelocity.y);
        _animator.SetFloat("RightMotion", _playerVelocity.x);
        _animator.SetBool("IsMoving", _isMoving);
    }
    private void CasualMovement(float dt)
    {
        if(_isMoving)
        {
            _animator.SetFloat("Speed", _speed, 0.1f, dt);
        }
        else
        {
            _animator.SetFloat("Speed", 0);
        }
    }

    private void SetAnimationActiveLayer(Animator animator, int layer, int transitionValue, float dt, float rateOfChange)
    {
        animator.SetLayerWeight(layer, Mathf.Lerp(animator.GetLayerWeight(layer), transitionValue, dt * rateOfChange));
    }

    private void SetCursorState(bool state)
    {
        Cursor.lockState = state ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
