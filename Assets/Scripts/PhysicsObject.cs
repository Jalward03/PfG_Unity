using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsObject : MonoBehaviour
{
    public Material awakeMaterial = null;
    public Material asleepMaterial = null;

    private Rigidbody rb = null;
    private bool isSleeping = false;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(rb.IsSleeping() && isSleeping && asleepMaterial != null)
        {
            isSleeping = false;
            GetComponent<MeshRenderer>().material = asleepMaterial;
        }

        if(!rb.IsSleeping() && !isSleeping && awakeMaterial != null)
        {
            isSleeping = true;
            GetComponent<MeshRenderer>().material = awakeMaterial;

        }
    }
}
