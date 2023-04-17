using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

using UnityEditor.Rendering;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace WipeOut
{
	public class BlackHole : MonoBehaviour
	{
		public bool hasFinished;

		public GameObject player;
		public Rigidbody ragdoll;
		public bool hasMoved;
		public Camera cam;
		public Transform newCameraPos;
		public bool finishedMoving;

		public Canvas hud;

		private void OnTriggerEnter(Collider other)
		{
			if(other.CompareTag("Player"))
			{
				// Moves player off screen when sucked into black hole and moves camera
				cam.GetComponent<CameraController>().enabled = false;

				ragdoll.transform.position = new Vector3(0, 1000, 0);
				StartCoroutine(MoveCamera());

				finishedMoving = true;
			}
		}

		private IEnumerator MoveCamera()
		{
			float t = 0;
			float moveTIme = 0.75f;

			Vector3 startPos = cam.transform.position;
			Vector3 endPos = newCameraPos.position;

			Quaternion startRot = cam.transform.rotation;
			Quaternion endRot = newCameraPos.rotation;

			while(t < moveTIme * 1.1f)
			{
				cam.transform.position = Vector3.Lerp(startPos, endPos, t / moveTIme);
				cam.transform.rotation = Quaternion.Slerp(startRot, endRot, t / moveTIme);

				yield return null;

				t += Time.deltaTime;
			}

			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			SceneManager.LoadScene("Menu");
		}

		private IEnumerator StartBlackHole()
		{
			float t = 0;
			float growSpeed = 0.75f;
			Vector3 starScale = transform.localScale;
			Vector3 endScale = new Vector3(50, 50, 50);

			while(t < growSpeed * 1.1f)
			{
				transform.localScale = Vector3.Lerp(starScale, endScale, t / growSpeed);

				yield return null;

				t += Time.deltaTime;
			}

			// Trigger added after the black hole fully grows to prevent an issue of trigger being different size to 
			// the mesh
			gameObject.AddComponent<SphereCollider>();
			gameObject.GetComponent<SphereCollider>().isTrigger = true;
			hasMoved = true;
			player.GetComponent<Ragdoll>().ragdollOn = true;
		}

		void Start()
		{
			// Setting each body part of ragdoll to not use gravity so player wouldn't end up falling in water
			ragdoll = player.GetComponent<Ragdoll>().rigidbodies[0];
			for(int i = 0; i < player.GetComponent<Ragdoll>().rigidbodies.Count; i++)
			{
				player.GetComponent<Ragdoll>().rigidbodies[i].useGravity = false;
			}

			StartCoroutine(StartBlackHole());
		}

		// Update is called once per frame

		private void FixedUpdate()
		{
			if(hasMoved && !finishedMoving)
			{
				// Adds force to ragdoll directly to the middle of black hole
				Vector3 playerToBlackHole = (transform.position - ragdoll.transform.position).normalized;

				ragdoll.AddForce(playerToBlackHole * 200.0f, ForceMode.Acceleration);
			}
		}
	}
}