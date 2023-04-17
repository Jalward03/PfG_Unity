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

		public BlackHole blackHole;
		public TextMeshProUGUI getUpText;
		[SerializeField] private GameObject hips;
		public List<Rigidbody> rigidbodies = new List<Rigidbody>();
		public bool canGetUp;

		public bool ragdollOn
		{
			get { return !animator.enabled; }
			set
			{
				cc.enabled = !value;
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
			
			// Waits 3 seconds before letting player get up
			canGetUp = true;

			yield return new WaitForSeconds(3.5f);

			if(ragdollOn && !blackHole.hasMoved)
				getUpText.enabled = true;
		}

		private void Update()
		{
			if(ragdollOn && !canGetUp)
			{
				StartCoroutine(GetUp());
			}

			// Teleports the player a little bit in the air and ragdoll is turned off
			if(getUpText.enabled && Input.GetKeyDown(KeyCode.F) && !blackHole.hasMoved)
			{
				getUpText.enabled = false;

				canGetUp = false;
				GetComponent<Ragdoll>().transform.position = rigidbodies[0].position;
				ragdollOn = false;
			}
		}
	}
}