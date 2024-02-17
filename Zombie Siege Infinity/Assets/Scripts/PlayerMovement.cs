using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller; //character controller component
    public float speed = 8f; //speed of the player
    public float crouchSpeed = 4f; //speed of the player when crouching
    public float gravity = -9.81f * 2; //gravity of the player
    public float jumpHeight = 3f; //jump height of the player
    public Transform groundCheck; //ground check object
    public float groundDistance = 20f; //ground distance
    public LayerMask groundMask; //ground mask

    private float originalHeight; // Original height of the character controller
    public float crouchHeight = 1f; // New height when crouching

    Vector3 velocity; //velocity of the player

    bool isGrounded; //is the player grounded
    bool isCrouching = false; //is the player crouching

    void Start() // Start is called before the first frame update
    {
        controller = GetComponent<CharacterController>(); //getting the character controller component
        originalHeight = controller.height; // Store original height
    }

    void Update()
    {
        //ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); //checking if the player is grounded

        //reset velocity
        if (isGrounded && velocity.y < 0) //if the player is grounded and the velocity is less than 0
        {
            velocity.y = -2f;
        }

        //getting input
        float x = Input.GetAxis("Horizontal"); //getting the horizontal input
        float z = Input.GetAxis("Vertical"); //getting the vertical input

        // Check if Shift is pressed
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            speed = isCrouching ? crouchSpeed : 16f; // Increase speed when Shift is held, but not if crouching
        }
        else
        {
            speed = isCrouching ? crouchSpeed : 12f; // Reset speed to normal or crouch speed when Shift is not held
        }

        // Crouch
    if (Input.GetKeyDown(KeyCode.LeftControl))
    {
        Crouch();
    }
    else if (Input.GetKeyUp(KeyCode.LeftControl))
    {
        StandUp();
    }


        // creating the movement vector
        Vector3 move = transform.right * x + transform.forward * z; // creating the movement vector 

        //moving the player
        controller.Move(move * speed * Time.deltaTime); //moving the player

        //checking if the player is jumping
        if (Input.GetButtonDown("Jump") && isGrounded && !isCrouching) //if the player presses the jump button and is grounded and not crouching
        {
            //actually jumping
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); //jumping
        }
        //falling velocity
        velocity.y += gravity * Time.deltaTime; //falling velocity

        //executing the jump
        controller.Move(velocity * Time.deltaTime); //executing the jump
    }

    void Crouch()
    {
        controller.height = crouchHeight; // Reduce the height of the character controller
        isCrouching = true; // Set the crouching flag to true
    }

    void StandUp()
    {
        controller.height = originalHeight; // Reset the height to the original value
        isCrouching = false; // Set the crouching flag to false
    }
}