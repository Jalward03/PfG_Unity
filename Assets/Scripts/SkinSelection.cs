using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using WipeOut;

namespace WipeOut
{
	public class SkinSelection : MonoBehaviour
	{

		[SerializeField] private CameraController cameraController;
		[SerializeField] private CharacterMover charMover;

		public Camera cam;
		public Transform endTransform;
		public Transform startTransform;

		public GameObject hud;
		public TimeTrial timer;
		public GameObject partyHat;
		public GameObject propellerHat;
		public GameObject cowboyHat;
		public GameObject sombreroHat;
		void Awake()
		{
			// Disables movement and 3rd person camera
			cameraController.canTurn = false;
			cameraController.canLookAtPlayer = false;
			charMover.canMove = false;
			cam = Camera.main;
			startTransform = Camera.main.transform;

		}

		public void ChangeHatOnClick(GameObject hat)
		{
		// disables all hats and sets new one to true
			RemoveAllHats();
			hat.SetActive(true);
		}

		public void RemoveHatOnClick()
		{
			RemoveAllHats();
		}
		public void StartGameOnClick()
		{
		// Enables player movement and 3rd person camera
			charMover.canMove = true;
			cameraController.canTurn = true;
			cameraController.canLookAtPlayer = true;
			cam.transform.position = endTransform.position;
			cam.transform.rotation = endTransform.rotation;

			
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			hud.SetActive(true);
			timer.timerCanStart = true;
			gameObject.SetActive(false);
		}

		public void RemoveAllHats()
		{
			propellerHat.SetActive(false);
			partyHat.SetActive(false);
		    cowboyHat.SetActive(false);
			sombreroHat.SetActive(false);
		}
	}
}