using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Rocket : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip levelCompleteSFX;

   
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] ParticleSystem levelCompleteParticles;

    AudioSource audioSource;
    

    enum State { Alive, Dying, Transcending }
    State state = State.Alive;



    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        Invoke("LoadNextLevel",levelLoadDelay);
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(deathSFX);
        deathParticles.Play();
        Invoke("LoadFirstLevel", levelLoadDelay);
    }

    private void LoadNextLevel()
    {
        int currentSceneInex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneInex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
        
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);

    }
}
