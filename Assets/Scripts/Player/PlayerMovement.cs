using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    public CharacterController characterController;

    [Header("Movement")]
    public float speed = 12f;
    public float gravity = -19.62f;
    public float totalJumps = 2f;
    public float jumps;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundLayerMask;
    bool isGrounded;

    [Header("Jump")]
    public float jumpHeight = 4f;   
    public float airControl = 0.3f;

    Vector3 velocity;

    private void Start()
    {
        isGrounded = false;
    }

    private void Update()
    {
        IsGrounded();
        Move();
        Jump();
        ApplyGravity();
    }

    private void IsGrounded()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayerMask);

        if (isGrounded && velocity.y < 0)
        {
            jumps = totalJumps;
            velocity.y = -2f;
        }
    }

    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Mathf.Clamp(moveX, -0.5f, 0.5f);
        Mathf.Clamp(moveZ, -0.5f, 0.5f);

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        if (move.magnitude >= 0.3)
        {
            move = move.normalized;
        }

        characterController.Move(move * speed * Time.deltaTime);

        if (velocity.y >= 0f)
        {
            move.x *= airControl;
            move.z *= airControl;
        }
    }

    private void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        velocity.x *= airControl;
        velocity.z *= airControl;
        characterController.Move(velocity * Time.deltaTime);
    }

    private void Jump()
    {
        if (isGrounded && Input.GetButtonDown("Jump") || Input.GetButtonDown("Jump") && jumps > 0)
        {
            velocity.y = 0f;
            jumps -= 1;
            velocity.y += Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
    }
}
