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
        if (Input.GetKey(KeyCode.W))
        {
            m_RB.AddForce(transform.forward * moveSpeed, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.S))
        {
            m_RB.AddForce(transform.forward * -moveSpeed, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.A))
        {
            m_RB.AddForce(transform.right * -moveSpeed, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.D))
        {
            m_RB.AddForce(transform.right * moveSpeed, ForceMode.Force);
        }
    }
}
