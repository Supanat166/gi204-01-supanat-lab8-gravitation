using System;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    private Rigidbody rb;

    private const float G = 0.006674f;
    public static List<Gravity> gravityObjectList;

    [SerializeField] private bool planets = false;
    [SerializeField] private int orbitSpeed = 1000;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (gravityObjectList == null)
        {
            gravityObjectList = new List<Gravity>();
        }

        gravityObjectList.Add(this);

        if (!planets)
        {
            rb.AddForce(Vector3.right * orbitSpeed);
        }
    
    }

    private void FixedUpdate()
    {
        foreach (var obj in gravityObjectList)
        {
            if (obj != this)
            {
                Attract(obj);
            }
        }
    }

    void Attract(Gravity other)
    {
        Rigidbody otherrb = other.rb;
        Vector3 direction = rb.position - otherrb.position;
        float distance = direction.magnitude;

        float forceMagnitude = G * (rb.mass * otherrb.mass/ Mathf.Pow(distance, 2));
        Vector3 gravityForce = forceMagnitude * direction.normalized;
        
        otherrb.AddForce(gravityForce);

    }
    
   
}
