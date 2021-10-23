using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform cameraTarget;
    public Vector3 cameraOffset;

    public float cameraSensitivity = 3f;
    public float smoothTime;


    private Vector3 m_velocity = Vector3.one;

    private float m_rotationX;
    private float m_rotationY;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    // Update is called once per frame
    void LateUpdate()
    {

        //Vector3 directionToTarget = transform.position - cameraTarget.transform.position;

        Vector3 cameraDestination = cameraTarget.position + cameraOffset;

        transform.position = Vector3.SmoothDamp(transform.position, cameraDestination, ref m_velocity, smoothTime);
        transform.LookAt(cameraTarget);       
    }
}
