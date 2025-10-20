using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public CharacterController controller;
    public Transform groundCheck;
    public float groundDistance = 0.3f;
    public LayerMask groundMask;

    public float speed = 15f;
    Vector3 velocity;
    public float gravity = -9.81f;
    public float jumpHeight = 10f;
    bool isGrounded;

    private bool frozen = false; // freeze the character during menu


    // Update is called once per frame
    void Update()
    {
        if (frozen) return;
        
            
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            if (Input.GetButton("Jump"))
            {
                velocity.y = jumpHeight;
            }
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    public void Freeze()
    {
        frozen = true;
        velocity = Vector3.zero; // immediately stop all movement
    }

    public void Unfreeze()
    {
        frozen = false;
    }
}
