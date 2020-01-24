using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip levelCompleteSFX;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] ParticleSystem levelCompleteParticles;

    AudioSource audioSource;
    Rigidbody myrigidbody;

    enum State { Alive, Dying, Transcending }
    State state = State.Alive;
    
    private void Start()
    {
        myrigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        RespondToRotateInput();
        RespondToThrust();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(state != State.Alive) { return; }


        switch (collision.gameObject.tag)
        {
            case "Friendly":
                    break;
            case "Landing Pad":
                StartSuccesSequence();
                break;
            case "Default":
                StartDeathSequence();
                break;
        }

    }

    private void StartSuccesSequence()
    {
        state = State.Transcending;
        audioSource.PlayOneShot(levelCompleteSFX);
        levelCompleteParticles.Play();
        Invoke("LoadNextLevel", 1f);
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(deathSFX);
        deathParticles.Play();
        Invoke("LoadFirstLevel", 1f);
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
        
    }
    
    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
        
    }

    private void RespondToRotateInput()
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

     private void RespondToThrust()
    {
        
        if (Input.GetKey(KeyCode.W))
        {
            ApplyThrust();
        }
        else
        {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }

    private void ApplyThrust()
    {
        float shipThrust = mainThrust * Time.deltaTime;
        myrigidbody.AddRelativeForce(Vector3.up * shipThrust);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        mainEngineParticles.Play();
    }
}
