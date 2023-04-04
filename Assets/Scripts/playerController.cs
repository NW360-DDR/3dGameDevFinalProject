using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private float moveSpeed = 5f;
    private float jumpForce = 10f;
    private float gravity = 20f;
    [SerializeField] private LayerMask groundLayer;
    public float rayHeight = 1.08f;

    private CharacterController controller;

    private Vector3 moveDirection = Vector3.zero;
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private bool isJumping = false;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 right = transform.TransformDirection(Vector3.right);

        moveDirection = (horizontal * right).normalized;
        moveDirection *= moveSpeed;

        // Checks to see if player is grounded before jumping
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDirection.y = jumpForce;
                isJumping = true;
            }
            else
            {
                isJumping = false;
            }
        }

        // Applies Gravity to the player
        moveDirection.y -= gravity * Time.deltaTime;
        
        // Applies movement force to the player
        controller.Move(moveDirection * Time.deltaTime);

        // Calls a raycast to check if player is grounded
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, rayHeight, groundLayer);

    }
}