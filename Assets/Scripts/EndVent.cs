using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WipeOut
{


	public class EndVent : MonoBehaviour
	{
		public GameObject particles;
		public Airvent airVent;

		private void OnTriggerEnter(Collider other)
		{
			if(other.CompareTag("Player"))
			{
				// Disables air vent when the next checkpoint was triggered
				particles.SetActive(false);
				airVent.enabled = false;
			}
		}
	}
}
