using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 1000f;

    float xRotation = 0f;
    float yRotation = 0f;

    public float topClamp = -90f;
    public float bottomClamp = 90f;

    // Locks the cursor to the middle of the screen, making it invisible.
    void Start()
    {   
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Gets the mouse inputs and applies rotation to the transform.
    void Update() // Update is called once per frame
    {
        // Get the horizontal mouse movement and apply sensitivity and time.
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        // Get the vertical mouse movement and apply sensitivity and time.
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Adjust the vertical rotation based on the mouse movement and clamp it within the specified range.
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp);

        // Adjust the horizontal rotation based on the mouse movement.
        yRotation += mouseX;

        // Apply the rotation to the transform.
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
