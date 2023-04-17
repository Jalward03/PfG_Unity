using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace WipeOut
{
	public class StartVent : MonoBehaviour
	{
		public GameObject particles;
		public Airvent airVent;

		private void OnTriggerEnter(Collider other)
		{
			if(other.CompareTag("Player"))
			{
				particles.SetActive(true);
				airVent.enabled = true;
			}
		}
	}
}