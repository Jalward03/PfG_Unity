using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

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

		public TimeTrial tt;

		private void OnTriggerEnter(Collider other)
		{
			if(other.CompareTag("Player"))
			{
				if(isWater)
				{
					// if player has fell in water player is teleported and any ragdoll variables are reset 
					// and set so you can re trigger the checkpoint
					player.GetComponent<CharacterController>().enabled = false;
					player.transform.position = player.GetComponent<CharacterMover>().repsawnPoint;
					player.GetComponent<Ragdoll>().transform.position = player.GetComponent<CharacterMover>().repsawnPoint;
					player.GetComponent<Ragdoll>().ragdollOn = false;
					player.GetComponent<Ragdoll>().canGetUp = false;
					player.GetComponent<Ragdoll>().getUpText.enabled = false;
					player.GetComponent<Ragdoll>().ragdollOn = false;
					hasUsed = true;
					player.GetComponent<CharacterController>().enabled = true;

					tt.m_seconds += 15;
				}
				else if(!hasUsed)
				{
					// if checkpoint already used player respawn point is set the the checkpoint transform
					player.GetComponent<CharacterMover>().repsawnPoint = transform.position;
				}
			}
			else if(other.CompareTag("Box") || other.CompareTag("Plank"))
			{
				if(isWater)
				{
					// pushable objects get teleported back to original position if falls in water
					other.transform.localPosition = other.transform.GetComponent<RespawnableObject>().localPos;
					other.transform.rotation = other.transform.GetComponent<RespawnableObject>().rot;
					other.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
					other.transform.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
				}
			}
		}
	}
}