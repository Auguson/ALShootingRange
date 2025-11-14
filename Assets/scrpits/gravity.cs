using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravity : MonoBehaviour
{
    [Header("Local Gravity Settings")]
    public Vector3 gravityDirection = new Vector3(0, -1, 0);
    public float gravityStrength = 9.81f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Disable Unity's built-in gravity
    }

    void FixedUpdate()
    {
        // Apply custom gravity
        rb.AddForce(gravityDirection.normalized * gravityStrength, ForceMode.Acceleration);
    }

    void Update()
    {
        // Optional: flip gravity with a key press
        if (Input.GetKeyDown(KeyCode.G))
        {
            gravityDirection = -gravityDirection;
        }
    }
}
