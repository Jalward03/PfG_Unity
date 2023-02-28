using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.VFX;

namespace WipeOut
{
	[RequireComponent(typeof(Animator))]
	public class Ragdoll : MonoBehaviour
	{
		private Animator animator = null;
		private CharacterController cc = null;
		private CameraController cameraController = null;

		private float getUpCooldown = 30.0f;
		public TextMeshProUGUI getUpText;
		[SerializeField] private GameObject hips;
		public List<Rigidbody> rigidbodies = new List<Rigidbody>();
		private bool canGetUp;

		public bool ragdollOn
		{
			get { return !animator.enabled; }
			set
			{
				cc.enabled = !value;
				//cameraController.canTurn = !value;
				animator.enabled = !value;
				foreach(Rigidbody rb in rigidbodies)
					rb.isKinematic = !value;
			}
		}

		// Start is called before the first frame update
		private void Start()
		{
			getUpText.enabled = false;
			cameraController = Camera.main.GetComponent<CameraController>();
			animator = GetComponent<Animator>();
			cc = GetComponent<CharacterController>();
			foreach(Rigidbody rb in rigidbodies)
				rb.isKinematic = false;

			ragdollOn = false;
		}

		private IEnumerator GetUp()
		{
			yield return new WaitForSeconds(3.5f);

			getUpText.enabled = true;
			canGetUp = true;
		}

		private void Update()
		{
			if(ragdollOn)
			{
				StartCoroutine(GetUp());
			}

			if(canGetUp && Input.GetKeyDown(KeyCode.F))
			{
				canGetUp = false;
				GetComponent<Ragdoll>().transform.position = rigidbodies[0].position;
				ragdollOn = false;
				getUpText.enabled = false;
			}
		}
	}
}