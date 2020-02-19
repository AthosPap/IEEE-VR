using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WC_Force : MonoBehaviour
{
    public float thrust = 1.0f;
    private Rigidbody rb;

    public Transform leftWheel, rightWheel;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.AddForce(0, 0, thrust, ForceMode.Impulse);
        //rb.AddForce(thrust * force_direction);
        Debug.Log(leftWheel.position);
        Debug.Log(rightWheel.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.U))
        {
            rb.AddForceAtPosition(leftWheel.forward * thrust, leftWheel.position, ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.I))
        {
            rb.AddForceAtPosition(rightWheel.forward * thrust, rightWheel.position, ForceMode.Impulse);
        }
    }
}