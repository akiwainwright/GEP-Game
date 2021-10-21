using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    
    private Rigidbody m_RB;
    // Start is called before the first frame update
    void Start()
    {
        m_RB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movementDirectionVec = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        movementDirectionVec.Normalize();

        //m_RB.MovePosition(transform.position + (movementDirectionVec * Time.deltaTime * moveSpeed));

        m_RB.velocity = movementDirectionVec * moveSpeed;

        if(movementDirectionVec != Vector3.zero)
        {
            transform.forward = movementDirectionVec;
        }
    }
}