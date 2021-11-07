using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
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
        #region Movement
        //Movement split up as horizontal behaviour will be different to vertical movement
        //Combination of vertical and horizontal movement will just combine the result of both 

        #region Combined Direction
        //If player is moving in a combination of a vertical and horizontal direction
        if (Input.GetAxis("Vertical") != 0 && Input.GetAxis("Horizontal") != 0)
        {
            Vector3 bothMovement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            bothMovement.Normalize();

            Quaternion facingRotation = Quaternion.LookRotation(bothMovement, Vector3.up);
            m_RB.velocity = bothMovement * moveSpeed;

            m_RB.AddForce(bothMovement, ForceMode.Force);

            transform.rotation = Quaternion.Slerp(transform.rotation, facingRotation, turnSpeed);
        }
        #endregion

        #region Forwards and Backwards
        //If player is moving in a vertical direction i.e forwards or backwards
        if (Input.GetAxis("Vertical") != 0 && Input.GetAxis("Horizontal") == 0)
        {
            Vector3 verticalMovement = new Vector3(0, 0, Input.GetAxis("Vertical"));
            verticalMovement.Normalize();

            Quaternion facingRotation = Quaternion.LookRotation(verticalMovement, Vector3.up);
            m_RB.velocity = verticalMovement * moveSpeed;

            m_RB.velocity = verticalMovement * moveSpeed;

            m_RB.AddForce(verticalMovement, ForceMode.Force);

            transform.rotation = Quaternion.Slerp(transform.rotation, facingRotation, turnSpeed);
        }
        #endregion

        #region Left and Right
        //If player is moving in a horizontal direction i.e left or right
        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") != 0)
        {
            Vector3 horizontalMovement = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            horizontalMovement.Normalize();

            Quaternion facingRotation = Quaternion.LookRotation(horizontalMovement, Vector3.up);
            m_RB.velocity = horizontalMovement * moveSpeed;

            m_RB.velocity = horizontalMovement * moveSpeed;

            m_RB.AddForce(horizontalMovement, ForceMode.Force);

            transform.rotation = Quaternion.Slerp(transform.rotation, facingRotation, turnSpeed);
        }
        #endregion

        #endregion

    }
}