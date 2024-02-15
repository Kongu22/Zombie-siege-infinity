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


    // Start is called before the first frame update
    void Start()
    {   
        //Lock the cursor to the middle of the screen making it invisible.
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Getting the mouse inputs.
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotation around the x axis (look up and down).
        xRotation -= mouseY;

        // Clamp the rotation.
        xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp);

        // Rotation around the y axis (Look left and right).
        yRotation += mouseX;

        // Apply rotations to our transform.
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
