using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using WipeOut;

using Random = System.Random;

namespace WipeOut
{
	public class Punching : MonoBehaviour
	{
		private Vector3 startPos;
		private float delay;
		private float punchCoolDown;
		private bool firstPunch;
		private bool canPunch;

		private void OnCollisionEnter(Collision collision)
		{
			// Checking if punching gloves hit player
			if(collision.gameObject.CompareTag("Player"))
			{
				if(collision.gameObject.GetComponent<Ragdoll>())
					collision.gameObject.GetComponent<Ragdoll>().ragdollOn = true;
			}
		}

		private IEnumerator Retract()
		{
			// Slowly moves each glove back to its original position after punching
			float t = 0;
			float retractTime = 1.0f;

			Vector3 start = transform.localPosition;

			while(t < retractTime * 1.1f)
			{
				transform.localPosition = Vector3.Lerp(start, startPos, t / retractTime);

				yield return null;

				t += Time.deltaTime;
			} 

			canPunch = true;
		}

		// Start is called before the first frame update
		void Start()
		{
			
			// Gives each punching glove a random start time to give variety on the timing
			startPos = transform.localPosition;
			delay = UnityEngine.Random.Range(1, 10);
			punchCoolDown = 2.5f;
		}

		void FixedUpdate()
		{
			// Timer cooldown started once retracted
			if(punchCoolDown > 0 && canPunch)
				punchCoolDown -= Time.fixedDeltaTime;

			// Adjusting the delay so it fits with the random starting the delay
			if(delay > 0 && !firstPunch)
				delay -= Time.fixedDeltaTime;

			if(delay <= 0)
			{
				firstPunch = true;
			}

			if(delay <= 0 || punchCoolDown <= 0)
			{
				// Adds high impulse force to simulate a real punch
				gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 2000, ForceMode.Impulse);
				punchCoolDown = 2.5f;
				delay = 10;
			}

			if(transform.localPosition.z >= 22.0f)
			{
				// Immediately stops the punch after a certain distance and starts retraction
				canPunch = false;

				gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
				StartCoroutine(Retract());
			}
		}
	}
}