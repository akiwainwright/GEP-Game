using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public GameObject target;

    public float distanceFromPlayer;

    public float cameraSensitivity = 3f;
    
    [Range(0, 1)]public float smooth = 0.1f;

    private float m_Smoother;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position, Vector3.up);        
    }
}
