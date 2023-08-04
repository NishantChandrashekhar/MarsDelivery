using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    [SerializeField] float upForce;
    [SerializeField] float rotationSensitity;
    [SerializeField] AudioClip thrustSound;
    [SerializeField] ParticleSystem leftThruster;
    [SerializeField] ParticleSystem rightThruster;
    [SerializeField] ParticleSystem booster;

    AudioSource audioSource;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = rb.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }
    void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * upForce * Time.deltaTime);
            // While the audioSource.Play() is called, the audio is played till its entirety.
            // Hence if the audioSource is triggered again in the next frame, it will overlap,
            // with the previous frames audio. Hence, we play only when the frame is not playing 
            // the audio.
            audioSource.clip = thrustSound;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            if(!booster.isPlaying)
                booster.Play();
        }
        else
        {
            // Stopping the audio as soon as the space is left.
            audioSource.Stop();
            booster.Stop();
        }
    }
    void Rotate()
    {
        float rotation = Input.GetAxis("Horizontal");
        TriggerSideParticles(rotation);
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward, -rotation * Time.deltaTime * rotationSensitity);
        rb.freezeRotation = false;
    }

    void TriggerSideParticles(float rotation)
    {
        if (rotation > 0)
        {
            if (!leftThruster.isPlaying)
                leftThruster.Play();
            rightThruster.Stop();
        }
        else if (rotation < 0)
        {
            leftThruster.Stop();
            if (!rightThruster.isPlaying)
                rightThruster.Play();
        }
        else
        {
            leftThruster.Stop();
            rightThruster.Stop();
        }
    }
}
