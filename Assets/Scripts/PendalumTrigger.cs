using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendalumTrigger : MonoBehaviour
{
    public List<Rigidbody> rigidbodies;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            foreach(Rigidbody rb in rigidbodies)
            {
                rb.constraints = RigidbodyConstraints.None;
            }
        }
    }
}
