// This script is inspired by the following tutorial:
// https://www.youtube.com/watch?v=_QajrabyTJc

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 8f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;

    Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;  // Radius of the sphere used
    public LayerMask groundMask;
    bool isGrounded; // To check if the player is on ground

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

         
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        // When running (2 times faster)
        if (isGrounded && Input.GetKey(KeyCode.LeftShift))
        {
            controller.Move(move * speed * 2 * Time.deltaTime);
        }

        // When jumping (using physics formula)
        if (isGrounded && Input.GetKey(KeyCode.Space))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }
}