using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlHorizontal : MonoBehaviour
{
    public float cameraSensitivity = 3f;
    
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * cameraSensitivity, Space.World);
    }
}
