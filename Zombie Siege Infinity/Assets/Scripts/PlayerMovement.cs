using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller; // Character controller component
    public float speed = 8f; // Speed of the player
    public float crouchSpeed = 4f; // Speed of the player when crouching
    public float gravity = -9.81f * 2; // Gravity of the player
    public float jumpHeight = 3f; // Jump height of the player
    public Transform groundCheck; // Ground check object
    public float groundDistance = 20f; // Ground distance
    public LayerMask groundMask; // Ground mask

    private float originalHeight; // Original height of the character controller
    public float crouchHeight = 1f; // New height when crouching

    Vector3 velocity; // Velocity of the player

    bool isGrounded; // Is the player grounded
    bool isCrouching = false; // Is the player crouching

    void Start() // Called before the first frame update
    {
        controller = GetComponent<CharacterController>(); // Get the character controller component
        originalHeight = controller.height; // Store the original height
    }

    // Update is called once per frame
    void Update()
    {
        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); // Check if the player is grounded

        // Reset velocity
        if (isGrounded && velocity.y < 0) // If the player is grounded and the velocity is less than 0
        {
            velocity.y = -2f;
        }

        // Get input
        float x = Input.GetAxis("Horizontal"); // Get the horizontal input
        float z = Input.GetAxis("Vertical"); // Get the vertical input

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
        if (Input.GetKeyDown(KeyCode.LeftControl)) // If the player presses the crouch button
        {
            Crouch(); // Call the crouch function
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            StandUp(); // Call the stand up function
        }

        // Create the movement vector
        Vector3 move = transform.right * x + transform.forward * z; // Create the movement vector 

        // Move the player
        controller.Move(move * speed * Time.deltaTime); // Move the player

        // Check if the player is jumping
        if (Input.GetButtonDown("Jump") && isGrounded && !isCrouching) // If the player presses the jump button and is grounded and not crouching
        {
            // Actually jump
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Jump
        }

        // Falling velocity
        velocity.y += gravity * Time.deltaTime; // Falling velocity

        // Execute the jump
        controller.Move(velocity * Time.deltaTime); // Execute the jump
    }

    // Check if the player is moving
    public bool IsMoving()
    {
        return Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f;
    }

    // Crouch function
    void Crouch()
    {
        controller.height = crouchHeight; // Reduce the height of the character controller
        isCrouching = true; // Set the crouching flag to true
    }

    // Stand up function
    void StandUp()
    {
        controller.height = originalHeight; // Reset the height to the original value
        isCrouching = false; // Set the crouching flag to false
    }
}