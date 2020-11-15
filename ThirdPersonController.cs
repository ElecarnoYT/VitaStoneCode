using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ThirdPersonController : MonoBehaviour
{
    // References
    public CharacterController controller;
    public Transform Cam;
    public Transform groundCheck;   

    // Movement Parameters
    public float originalSpeed = 8f;
    public float speed = 8f;
    public float sprintSpeedMultiplier = 2f;
    public float crouchSpeedDivider = 2f;
    public float slideFriction = 15f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    
    //Turn Smoothing
    public float turnSmoothTime = 0.05f;
    float turnSmoothVelocity;

    //Gravity + Groundcheck
    Vector3 velocity;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;

    // Status
    public bool isMove;
    public bool isGrounded;
    public bool isSprint;
    public bool isCrouch;
    public bool isSlide;
    public bool isJump;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update ()
    {
        // Base Systems
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);

            isMove = true;
        }
        else
        {
            isMove = false;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Slide Friction
        if (isSlide)
        {

            speed -= slideFriction * Time.deltaTime;

            // Reset to Crouch
            if (speed <= 0)
            {
                controller.height = 1.8f;
                speed = originalSpeed / crouchSpeedDivider;
                isCrouch = true;
                isSlide = false;
            }
        }       

        // - Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            if (!isCrouch)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }

        // - Sprint
        if (Input.GetKeyDown("left shift"))
        {
            speed = speed * sprintSpeedMultiplier;
            isSprint = true;
            isSlide = false;

            if (isCrouch)
            {
                controller.height = 3.6f;
                speed = originalSpeed * sprintSpeedMultiplier;
                isCrouch = false;
                isSlide = false;
            }
        }
        if (Input.GetKeyUp("left shift"))
        {
            speed = originalSpeed;
            isSprint = false;
            isSlide = false;

            if (isCrouch)
            {
                speed = originalSpeed / crouchSpeedDivider;
                isSlide = false;
            }
        }
        
        // - Crouch
        if (Input.GetKeyDown("left ctrl"))
        {
            controller.height = 2.8f;
            speed = originalSpeed / crouchSpeedDivider;
            isCrouch = true;
            
            // - Slide (Crouch + Sprint)
            if (isSprint)
            {
                speed = originalSpeed * sprintSpeedMultiplier * 2;
                isSlide = true;
            }
        }

        if (Input.GetKeyUp("left ctrl"))
        {
            controller.height = 3.6f;
            speed = originalSpeed;
            isCrouch = false;
            isSlide = false;

            if (isSprint)
            {
                speed = originalSpeed * sprintSpeedMultiplier;
                isSlide = false;
            }
        }
    }
}