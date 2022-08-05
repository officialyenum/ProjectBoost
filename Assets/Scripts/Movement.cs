using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // PARAMETERS - for tuning, typically set in the editor
    [SerializeField] AudioClip thrustSFX;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 1000f;

    [SerializeField] ParticleSystem bottomParticles;
    [SerializeField] ParticleSystem leftParticles;
    [SerializeField] ParticleSystem rightParticles;
    // CACHE - e.g. reference for readability or speed
    Rigidbody rb;
    AudioSource audioSource;
    // STATE - Private instance (member) variables
    bool isAlive;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrust();
        }
        else
        {
            StopThrust();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey("left") || Input.GetKey(KeyCode.A))
        {
            // rotate left
            RotateLeft();
        }
        else if (Input.GetKey("right") || Input.GetKey(KeyCode.D))
        {
            // rotate right
            RotateRight();
        }
        else
        {
            RotateStop();
        }

    }

    void StartThrust()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        // Play thrust sound
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(thrustSFX);
        }
        // play particle system
        if (!bottomParticles.isPlaying)
        {
            bottomParticles.Play();
        }
    }

    void StopThrust()
    {
        audioSource.Stop();
        bottomParticles.Stop();
    }

    void RotateLeft()
    {
        ApplyRotateThrust(rotationThrust);
        if (!leftParticles.isPlaying)
        {
            leftParticles.Play(); // Play the particle system if it's not already playing
        }
    }

    void RotateRight()
    {
        ApplyRotateThrust(-rotationThrust);
        if (!rightParticles.isPlaying)
        {
            rightParticles.Play(); // Play the particle system if it's not already playing
        }
    }

    void RotateStop()
    {
        leftParticles.Stop(); // stop the particles
        rightParticles.Stop(); // stop the particles
    }

    void ApplyRotateThrust(float rotateThrust)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotateThrust * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }
}
