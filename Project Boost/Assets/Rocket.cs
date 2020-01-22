using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField]
    float rcsThrust = 100f;
    [SerializeField]
    float mainThrust = 100f;
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

        
        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            
            transform.Rotate(Vector3.forward * rotationThisFrame);

        }
        else if (Input.GetKey(KeyCode.D)) 
        {
            
            transform.Rotate(-Vector3.forward * rotationThisFrame); 
        }
        myrigidbody.freezeRotation = false;

    }

    private void Thrust()
    {
        float shipThrust = mainThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.W))
        {
            myrigidbody.AddRelativeForce(Vector3.up * shipThrust);
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
