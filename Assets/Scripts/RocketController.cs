using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    [SerializeField] float upForce;
    [SerializeField] float rotationSensitity;
    [SerializeField] AudioClip thrustSound;

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
        }
        else
        {
            // Stopping the audio as soon as the space is left.
            audioSource.Stop();
        }
    }
    void Rotate()
    {
        float rotation = Input.GetAxis("Horizontal");
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward, -rotation * Time.deltaTime * rotationSensitity);
        rb.freezeRotation = false;
    }
}
