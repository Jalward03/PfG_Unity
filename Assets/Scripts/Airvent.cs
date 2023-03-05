using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using WipeOut;

namespace WipeOut
{
	
	public class Airvent : MonoBehaviour
	{
		// Start is called before the first frame update
		void Start() { }

		// Update is called once per frame
		void FixedUpdate()
		{
			RaycastHit hit;
			if(Physics.BoxCast(transform.position - 2 * transform.forward, new Vector3(15, 2, 1), transform.forward, out hit, transform.rotation, 75))
			{
				if(hit.transform.CompareTag("Player"))
				{
					hit.transform.GetComponent<Ragdoll>().ragdollOn = true;
					hit.transform.GetComponent<Ragdoll>().rigidbodies[0].AddForce(transform.forward * 700.0f, ForceMode.Impulse);
				}
			}


		}
	}
}