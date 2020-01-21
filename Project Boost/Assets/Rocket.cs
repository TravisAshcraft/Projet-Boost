﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    AudioSource audioSource;
    Rigidbody myrigidbody;

    private void Start()
    {
        myrigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            else
            {
                audioSource.Stop();
            }
            
        }
        if (Input.GetKey(KeyCode.A)) 
        {
            transform.Rotate(Vector3.forward);

        }
       else if (Input.GetKey(KeyCode.D)) { transform.Rotate(-Vector3.forward); }

        
    }
}
