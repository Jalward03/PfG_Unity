using System;
using System.Collections;
using System.Collections.Generic;
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

        public List<Rigidbody> rigidbodies = new List<Rigidbody>();

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
            cameraController = Camera.main.GetComponent<CameraController>();
            animator = GetComponent<Animator>();
            cc = GetComponent<CharacterController>();
            foreach(Rigidbody rb in rigidbodies)
                rb.isKinematic = false;

            ragdollOn = false;
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.F) && ragdollOn)
            {
                for(int i = 0; i < rigidbodies.Count; i++)
                {
                    if(rigidbodies[i].IsSleeping()) 
                        continue;
                }
                
            }
        }
    }
}
