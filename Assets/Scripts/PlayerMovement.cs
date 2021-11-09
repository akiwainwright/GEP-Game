using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform playerCamera;
    public float moveSpeed = 5f;
    [Range(0,1)]public float turnSpeed = 0.5f;
    
    private Rigidbody m_RB;
    // Start is called before the first frame update
    void Start()
    {
        m_RB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            //Moving based on camera dimensions
            Vector3 horizontalMovement = playerCamera.right * Input.GetAxis("Horizontal");
            Vector3 verticalMovement = Vector3.ProjectOnPlane(playerCamera.forward, Vector3.up) * Input.GetAxis("Vertical");

            Vector3 totalMovement = horizontalMovement + verticalMovement;
            totalMovement.Normalize();

            m_RB.velocity = totalMovement * moveSpeed * Time.fixedDeltaTime;

            m_RB.transform.forward = totalMovement;
        }
    }
}