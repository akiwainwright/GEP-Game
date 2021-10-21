using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public GameObject player;
    public GameObject cameraTarget;
    public Vector3 cameraOffset;

    public float cameraSensitivity = 3f;
    


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

        Vector3 directionToTarget = transform.position - cameraTarget.transform.position;


        
        transform.position = player.transform.localPosition + cameraOffset;
        
    }
}
