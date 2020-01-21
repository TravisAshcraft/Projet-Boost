﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float thrustSpeed = 10f;
    Rigidbody myrigidbody;

    private void Start()
    {
        myrigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            myrigidbody.AddRelativeForce(Vector3.up);
        }
        if (Input.GetKey(KeyCode.A)) 
        { 
            print("Rotate Left");

        }
       else if (Input.GetKey(KeyCode.D)) { print("Roatate Right"); }

        
    }
}