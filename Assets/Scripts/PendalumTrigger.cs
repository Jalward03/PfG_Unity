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
            // Allows the wrecking ball to start swinging when player gets close so they have not  
            // lost all momentum when the player reaches this stage
            foreach(Rigidbody rb in rigidbodies)
            {
                rb.constraints = RigidbodyConstraints.None;
            }
        }
    }
}
