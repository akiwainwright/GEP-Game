using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraControl : MonoBehaviour
{
    public float cameraSensitivity = 3f;
    public bool inverted = true;


    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * cameraSensitivity, Space.World);

        /*if (inverted)
        {
            transform.Rotate(Vector3.right, Input.GetAxis("Mouse Y") * cameraSensitivity, Space.Self);
        }
        else
        {
            transform.Rotate(Vector3.right, -Input.GetAxis("Mouse Y") * cameraSensitivity, Space.Self);
        }*/
    }
}
