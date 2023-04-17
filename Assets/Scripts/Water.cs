using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using WipeOut;

namespace WipeOut
{
	public class Water : MonoBehaviour
	{
		private void OnTriggerEnter(Collider other)
		{
			if(other.CompareTag("Player"))
			{
				other.transform.position = other.GetComponent<CharacterMover>().repsawnPoint;
			}
		}
	}
}