using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public float mouseSensitivity = 10f;

    public Transform playerBody;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Press ENTER to enable mouselook. Press ESC to turn it back off.");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Return)) { Cursor.lockState = CursorLockMode.Locked; }
        if (Input.GetKey(KeyCode.Escape)) { Cursor.lockState = CursorLockMode.None; }

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            playerBody.Rotate(Vector3.up * mouseX);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
    }
}
