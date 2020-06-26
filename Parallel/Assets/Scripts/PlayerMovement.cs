using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    CharacterController controller;

    [SerializeField]
    float speed = 3f;
    [SerializeField]
    float sprintSpeed = 4.5f;

    [SerializeField]
    float gravity = -19.62f;

    Vector3 velocity;
    Transform groundCheck;

    [SerializeField]
    float groundDistance = 0.4f;

    [SerializeField]
    LayerMask groundMask;
    bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        groundCheck = transform.Find("Ground Check").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Gravity & movement
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        // directional movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (Input.GetKey(KeyCode.LeftShift)) controller.Move(move * sprintSpeed * Time.deltaTime);
        else controller.Move(move * speed * Time.deltaTime);
    }
}
