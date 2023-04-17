using System;
using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;

namespace WipeOut
{
	public class RagdollCollision : MonoBehaviour
	{
		private void OnCollisionEnter(Collision collision)
		{
			Ragdoll ragdoll = collision.gameObject.GetComponentInParent<Ragdoll>();
			if(ragdoll != null)
			{
				ragdoll.ragdollOn = true;
			}
		}
	}
}