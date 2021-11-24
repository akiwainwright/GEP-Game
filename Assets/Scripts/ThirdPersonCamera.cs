using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
   
    [SerializeField] private Transform m_Target;


    private Vector3 m_Velocity = Vector3.zero;

    [Header("Camera Movement")]
    public float keepDistBehind = 6f;
    public float keepDistanceAbove = 2.5f;
    public float smooth = 0.3f;
    public float camSensitivity = 3f;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        //Camera Initial Position
        transform.position = new Vector3(m_Target.position.x, m_Target.position.y + keepDistanceAbove, m_Target.position.z - keepDistBehind);
        transform.rotation = Quaternion.LookRotation((m_Target.position - transform.position).normalized);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        #region IntentCam

        //Horizontal intent
        if (Input.GetAxis("Mouse X") != 0)
        {
            float angle = Input.GetAxis("Mouse X");
            if (angle < 0)
            {
                angle += 360;
            }
            else if (angle > 360)
            {
                angle -= 360;
            }
            Vector3 fromTarget = transform.position - m_Target.position;
            Vector3 nextVec = Quaternion.AngleAxis(angle, m_Target.up) * fromTarget;
            Vector3 nextPos = m_Target.position + (nextVec.normalized * Mathf.Sqrt((keepDistanceAbove * keepDistanceAbove) + (keepDistBehind * keepDistBehind)));
            transform.position = Vector3.MoveTowards(transform.position, nextPos, angle * camSensitivity * Time.deltaTime);
        }

        if (Input.GetAxis("Mouse Y") != 0)
        {
            float angle = Input.GetAxis("Mouse Y");

            if (angle < 0)
            {
                angle += 360;
            }
            else if (angle > 360)
            {
                angle -= 360;
            }

            float angleToCorrectAxis = ( (Vector3.Dot(m_Target.right, Vector3.right)) / (m_Target.right.magnitude * Vector3.right.magnitude) );

            Vector3 playerHorizontalAxis = Quaternion.AngleAxis(-angleToCorrectAxis, m_Target.up) * m_Target.right;

            Vector3 fromTarget = transform.position - m_Target.position;

            Vector3 nextVec = Quaternion.AngleAxis(angle, playerHorizontalAxis) * fromTarget;

            Vector3 nextPos = m_Target.position + (nextVec.normalized * Mathf.Sqrt((keepDistanceAbove * keepDistanceAbove) + (keepDistBehind * keepDistBehind)));

            RaycastHit rHit;

            if (Physics.Linecast(m_Target.position, nextPos, out rHit))
            {
                float distance = (rHit.point - m_Target.position).magnitude * 0.75f;
                nextPos = m_Target.position + (nextVec.normalized * distance);
            }

            transform.position = Vector3.MoveTowards(transform.position, nextPos, angle * camSensitivity * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation((m_Target.position - transform.position).normalized);
        }
        #endregion

        #region Auto Cam
        
        transform.rotation = Quaternion.LookRotation((m_Target.position - transform.position).normalized);

        //Camera doesn't move on Y axis so working on y plane to prevent changing the Y position
        Vector3 m_CamPosYPlane = Vector3.ProjectOnPlane(transform.position, Vector3.up);
        Vector3 m_FocusPointYPlane = Vector3.ProjectOnPlane(m_Target.position, Vector3.up);

        //Calculating the next position for the camera on the Y plane using player to camera direction and desired keep distance
        Vector3 yPlaneNewCamPos = m_FocusPointYPlane + ((m_CamPosYPlane - m_FocusPointYPlane).normalized * keepDistBehind);

        //Getting new position for camera by changing y value to cameras original y value
        Vector3 newCamPos = new Vector3(yPlaneNewCamPos.x, transform.position.y, yPlaneNewCamPos.z);

        RaycastHit hit;

        if (Physics.Linecast(m_Target.position, transform.position, out hit))
        {
            Vector3 vecToHit = hit.point - m_Target.position;
            float distance = vecToHit.magnitude * 0.75f;
            newCamPos = m_Target.position + (vecToHit.normalized) * distance;
        }

        transform.position = Vector3.SmoothDamp(transform.position, newCamPos, ref m_Velocity, smooth * Time.deltaTime);

        #endregion
    }

}
