using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainEngineThrustersParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;

    AudioSource sound;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sound = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }

        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    void ApplyRotaion(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }

    void StartThrusting()
    {
        //Debug.Log("Thrust");
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        if (!sound.isPlaying)
        {
            sound.PlayOneShot(mainEngine);
        }
        if (!mainEngineThrustersParticles.isPlaying)
        {
            mainEngineThrustersParticles.Play();
        }
    }

    private void StopThrusting()
    {
        sound.Stop();
        mainEngineThrustersParticles.Stop();
    }

    void RotateLeft()
    {
        //Debug.Log("Left");
        ApplyRotaion(rotationThrust);

        if (!leftThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Play();
        }
    }

    void RotateRight()
    {
        //Debug.Log("Right");
        ApplyRotaion(-rotationThrust);

        if (!rightThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Play();
        }
    }

    void StopRotating()
    {
        rightThrusterParticles.Stop();
        leftThrusterParticles.Stop();
    }
}
