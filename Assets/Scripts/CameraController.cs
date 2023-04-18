using System;
using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;

namespace WipeOut
{
	public class CameraController : MonoBehaviour
	{
		public Transform target = null;
		public float speed = 180;
		public float distance = 5;
		public float pullBackSpeed = 8;
		public float zoomSpeed = 8;
		private float currentDistance = 0;
		private float heightOffset = 1.5f;
		public bool canTurn = true;
		public bool canLookAtPlayer = true;

		Vector3 GetTargetPosition()
		{
			return target.position + heightOffset * Vector3.up - new Vector3(0, 0.5f, 0);
		}

	

		void Update()
		{
			if(canTurn)
			{
				Vector3 angles = transform.eulerAngles;
				float dx = -Input.GetAxis("Mouse Y");
				float dy = Input.GetAxis("Mouse X");

				angles.x = Mathf.Clamp(angles.x + dx * speed * Time.deltaTime, 0, 70);

				angles.y += dy * speed * Time.deltaTime;
				transform.eulerAngles = angles;
			}

			// right drag rotates the camera

			if(canLookAtPlayer)
			{
				RaycastHit hit;

				if(Physics.Raycast(GetTargetPosition(), -transform.forward, out hit, distance) && hit.collider.gameObject.layer != LayerMask.NameToLayer("Player"))
				{
					currentDistance = hit.distance;
				}
				else
				{
					currentDistance = Mathf.MoveTowards(currentDistance, distance, Time.deltaTime * pullBackSpeed);
				}

				transform.position = GetTargetPosition() - currentDistance * transform.forward;
			}
		}
	}
}