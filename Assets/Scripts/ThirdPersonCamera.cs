using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public GameObject target;
    public Vector3 cameraOffset;

    public float cameraSensitivity = 3f;
    
    [Range(0, 1)]public float smooth = 0.1f;

    private float m_Smoother;
    


    private Vector3 m_velocity = Vector3.zero;

    private float m_rotationX;
    private float m_rotationY;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        //using exponential smoothing to prevent jitter
        m_Smoother = 1f - Mathf.Pow(smooth, Time.deltaTime);

    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 destination = target.transform.position + cameraOffset;
        //gameObject.transform.position = destination;

        
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, destination, m_Smoother);
        
        
    }
}
