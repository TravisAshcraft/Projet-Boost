using System;
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
        Rotate();
        Thrust();
    }

    private void Rotate()
    {
        myrigidbody.freezeRotation = true;
        
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward);

        }
        else if (Input.GetKey(KeyCode.D)) 
        { 
            transform.Rotate(-Vector3.forward); 
        }
        myrigidbody.freezeRotation = false;

    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.W))
        {
            myrigidbody.AddRelativeForce(Vector3.forward);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            else
            {
                audioSource.Stop();
            }

        }
    }
}
