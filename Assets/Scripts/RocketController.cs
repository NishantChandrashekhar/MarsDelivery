using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    [SerializeField] float upForce;
    [SerializeField] float rotationSensitity;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
