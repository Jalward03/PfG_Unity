using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace WipeOut
{
	public class RaycastImpusle : MonoBehaviour
	{
		public float hitForce = 500;

		void Update()
		{
			if(Input.GetMouseButtonDown(0))
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hitPoint;
				if(Physics.Raycast(ray, out hitPoint, 500))
				{
					Ragdoll ragdoll = hitPoint.collider.GetComponentInParent<Ragdoll>();
					if(ragdoll != null)
					{
						ragdoll.ragdollOn = true;
						ragdoll.GetComponentInParent<Rigidbody>().AddForce(Vector3.up * 100, ForceMode.Impulse);
					}

					Rigidbody rb = hitPoint.collider.GetComponent<Rigidbody>();
					if(rb != null)
						rb.AddForce(ray.direction * hitForce);
				}
			}

			if(Input.GetMouseButtonDown(1))
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hitPoint;
				if(Physics.Raycast(ray, out hitPoint, 500) && hitPoint.rigidbody)
				{
					hitPoint.rigidbody.constraints = RigidbodyConstraints.None;
				}
			}
		}
	}
}