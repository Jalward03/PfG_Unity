using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Pushing : MonoBehaviour
{
	// Start is called before the first frame update

	public Animator animator;
	public CharacterController cc;

	private bool isPushing;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		cc = GetComponent<CharacterController>();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		// Sending a short ray about arms height and reaches out about arms length
		Ray ray = new Ray(cc.transform.position, cc.transform.forward);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, 3))
		{
			// Detects the tag of detected object and adds corresponding force and stata push animation
			if(hit.transform.CompareTag("Box"))
			{
				hit.transform.GetComponent<Rigidbody>().AddForce(ray.direction * 5f, ForceMode.Acceleration);
				animator.SetBool("Pushing", true);
			}
			else if(hit.transform.CompareTag("Plank"))
			{
				hit.transform.GetComponent<Rigidbody>().AddForce(ray.direction * 1.5f, ForceMode.Impulse);
				animator.SetBool("Pushing", true);
			}
		}
		else
		{
			animator.SetBool("Pushing", false);
		}
	}
}