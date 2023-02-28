using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using WipeOut;

namespace WipeOut
{
	public class CheckpointTrigger : MonoBehaviour
	{
		public bool isWater;
		private bool hasUsed;
		public GameObject player;

		private void OnTriggerEnter(Collider other)
		{
			if(other.CompareTag("Player"))
			{
				if(isWater)
				{
					player.GetComponent<CharacterController>().enabled = false;
					player.transform.position = player.GetComponent<CharacterMover>().repsawnPoint;
					player.GetComponent<Ragdoll>().transform.position = player.GetComponent<CharacterMover>().repsawnPoint;
					player.GetComponent<Ragdoll>().ragdollOn = false;
					hasUsed = true;
					player.GetComponent<CharacterController>().enabled = true;
					Debug.Log("lmao");

				}
				else if(!hasUsed)
				{
					player.GetComponent<CharacterMover>().repsawnPoint = transform.position;
				}
			}
		}
	}
}