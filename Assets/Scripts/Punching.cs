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
			if(collision.gameObject.CompareTag("Player"))
			{
				
				collision.gameObject.GetComponent<Ragdoll>().ragdollOn = true;
				//collision.gameObject.GetComponent<CharacterController>().enabled = false;
				//collision.gameObject.GetComponentInParent<CharacterController>().enabled = false;
			}
		}

		private IEnumerator Retract()
		{
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
			startPos = transform.localPosition;
			delay = UnityEngine.Random.Range(10, 100);
			punchCoolDown = 25.0f;
		}

		// Update is called once per frame
		void Update()
		{
		
			if(punchCoolDown > 0 && canPunch)
				punchCoolDown -= Time.fixedDeltaTime;

			if(delay > 0 && !firstPunch)
				delay -= Time.fixedDeltaTime;

			if(delay <= 0)
			{
				firstPunch = true;
				
			}
			
			if(delay <= 0 || punchCoolDown <= 0)
			{
				
				gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 2000, ForceMode.Impulse);
				punchCoolDown = 25.0f;
				delay = 100;
			}

			if(transform.localPosition.z >= 22.0f)
			{
				canPunch = false;

				gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
				//transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 21f);
				StartCoroutine(Retract());

			}
		}
	}
}