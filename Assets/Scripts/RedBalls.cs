using System;
using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;

namespace WipeOut
{
	public class RedBalls : MonoBehaviour
	{
		[SerializeField] private GameObject player;

		private void OnTriggerEnter(Collider other)
		{
			if(other.CompareTag("Player"))
			{
				player.GetComponent<Ragdoll>().ragdollOn = true;
				Vector3 dirToBall = (player.transform.position - gameObject.transform.position).normalized;
				player.GetComponent<Ragdoll>().rigidbodies[0].AddForce(dirToBall * 100, ForceMode.Impulse);
			}
		}
	}
}