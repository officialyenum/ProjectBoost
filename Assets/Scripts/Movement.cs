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
            Thrust();
        }
        else
        {
            audioSource.Stop();
            bottomParticles.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey("left") || Input.GetKey(KeyCode.A))
        {
            leftParticles.Play();
            RotateThrust(rotationThrust);
        }
        else if (Input.GetKey("right") || Input.GetKey(KeyCode.D))
        {
            rightParticles.Play();
            RotateThrust(-rotationThrust);
        }
        else
        {
            leftParticles.Stop();
            rightParticles.Stop();
        }

    }

    void Thrust()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(thrustSFX);
            bottomParticles.Play();
            // audioSource.Play(thrustSFX);
        }
    }

    void RotateThrust(float rotateThrust)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotateThrust * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }
}
