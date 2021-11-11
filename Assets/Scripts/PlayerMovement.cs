using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform playerCamera;

    private Rigidbody m_RB;

    private Vector3 moveDir;

    public float speed = 5f;

    private void Start()
    {
        m_RB = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            Vector3 horizontalDir = playerCamera.right * Input.GetAxis("Horizontal");

            Vector3 verticalDir = Vector3.ProjectOnPlane(playerCamera.forward, Vector3.up) * Input.GetAxis("Vertical");

            moveDir = (horizontalDir + verticalDir).normalized;

            m_RB.velocity = moveDir * speed * Time.deltaTime;
            
        }

        if (moveDir != Vector3.zero)
        {
            m_RB.rotation = Quaternion.LookRotation(moveDir, Vector3.up);
        }
    }


}