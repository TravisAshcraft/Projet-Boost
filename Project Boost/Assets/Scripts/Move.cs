using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] float rcsThrust = 10f;
    [SerializeField] float mainThrust = 100f;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] AudioClip mainEngine;

    AudioSource audioSource;
    Rigidbody myrigidbody;

    public Joystick horizontalJoystick;
    public JoyButton verticalJoyButton;

    enum State { Alive, Dying, Transcending }
    State state = State.Alive;


    // Start is called before the first frame update
    void Start()
    {
        myrigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        verticalJoyButton = FindObjectOfType<JoyButton>();
        horizontalJoystick = FindObjectOfType<Joystick>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            RespondToRotateInput();
            ApplyThrust();
        }
    }

   

    public void RespondToRotateInput()
    {
        transform.Rotate(0, 0, horizontalJoystick.Horizontal * rcsThrust);
    }

    public  void ApplyThrust()
    {
        

        if (verticalJoyButton.Pressed)
        {
            
            float shipThrust = mainThrust * Time.deltaTime;
            myrigidbody.AddRelativeForce(Vector3.up * shipThrust);
            mainEngineParticles.Play();
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }

        }
        else
        {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
        
    }
}
