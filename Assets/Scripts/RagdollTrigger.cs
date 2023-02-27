using System;
using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;

namespace WipeOut
{
	

public class RagdollTrigger : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		Ragdoll ragdoll = other.GetComponentInParent<Ragdoll>();
		if(ragdoll != null)
			ragdoll.ragdollOn = true;
	}


}
}
