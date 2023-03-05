using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

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
					player.GetComponent<Ragdoll>().canGetUp = false;
					player.GetComponent<Ragdoll>().getUpText.enabled = false;
					player.GetComponent<Ragdoll>().ragdollOn = false;
					hasUsed = true;
					player.GetComponent<CharacterController>().enabled = true;

				}
				else if(!hasUsed)
				{
					player.GetComponent<CharacterMover>().repsawnPoint = transform.position;
				}
			}
			else if(other.CompareTag("Box"))
			{
				if(isWater)
				{
					other.transform.localPosition = other.transform.GetComponent<RespawnableObject>().localPos;
					other.transform.rotation = other.transform.GetComponent<RespawnableObject>().rot;
					other.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
					other.transform.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
					
				}

			}
		}
	}
}