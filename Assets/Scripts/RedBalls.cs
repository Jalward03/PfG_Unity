using System;
using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;

namespace WipeOut
{
	public class RedBalls : MonoBehaviour
	{

		private void OnTriggerEnter(Collider other)
		{
			if(other.CompareTag("Player"))
			{
				if(other.GetComponent<CharacterMover>())
					other.GetComponent<CharacterMover>().velocity = other.GetComponent<CharacterMover>().velocity * -1;
			}
		}
	}
}