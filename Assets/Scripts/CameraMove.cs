using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public float mouseXSensitivity = 100f;
    public float mouseYSensitivity = 150f;

    public Transform playerBody;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.LogWarning("the camera move script is being called!! " +
                  "so far this is assuming we're starting ingame in gameplay. " +
                  "this (and all other active motion code like player movement or dinosaur movement or " +
                  "whatever or anything's animations, probably) should ONLY run during ingame gameplay! " +
                  "so in the future be sure to make sure these update scripts are only being run in the right global game state.");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) { Cursor.lockState = CursorLockMode.None; }

        if (Cursor.lockState == CursorLockMode.Locked)
        //engine error unsure how to fix - alt/tabbing to switch windows
        //will free the cursor from lock state, but cursor.lockState sometimes remains
        //"Locked", so the player can still move around the cursor. detecting when the game is unfocused may need to happen separately i suppose
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseXSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseYSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            playerBody.Rotate(Vector3.up * mouseX);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }

        if (Cursor.lockState == CursorLockMode.None)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mpos = Input.mousePosition;
                if (mpos.x > 0 && mpos.x < Screen.width && mpos.y > 0 && mpos.y < Screen.height)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    // where we would move from a menu state to ingame/gameplay state, if the player tabs out i guess
                }
            }
        }
    }
}
