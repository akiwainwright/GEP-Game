using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlVertical : MonoBehaviour
{
    public float cameraSensitivity = 3f;
    public bool inverted = false;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    // Update is called once per frame
    void LateUpdate()
    {
        if (inverted)
        {
            transform.Rotate(Vector3.right, Input.GetAxis("Mouse Y") * cameraSensitivity, Space.Self);
        }
        else
        {
            transform.Rotate(Vector3.right, Input.GetAxis("Mouse Y") * -cameraSensitivity, Space.Self);
        }
    }
}
