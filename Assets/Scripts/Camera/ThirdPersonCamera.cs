using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private GameObject m_GameManager;
   
    [SerializeField] private Transform m_Target;


    private Vector3 m_Velocity = Vector3.zero;

    [Header("Camera Offset")]
    [SerializeField]private float m_KeepDistanceBehind = 6f;
    [SerializeField]private float keepDistanceAbove = 2.5f;

    [SerializeField] private float m_CamSpeed = 5f;
    public float camSensitivity = 3f;
    private float m_HorizontalAngle;


    // Start is called before the first frame update
    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;

        //Camera Initial Position
        transform.position = new Vector3(m_Target.position.x, m_Target.position.y + keepDistanceAbove, m_Target.position.z - m_KeepDistanceBehind);
        transform.rotation = Quaternion.LookRotation((m_Target.position - transform.position).normalized);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!m_GameManager.GetComponent<GameManager>().paused)
        {
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            if (Cursor.lockState == CursorLockMode.Locked)
            {

                #region IntentCam

                //Horizontal intent
                if (Input.GetAxis("Mouse X") != 0)
                {
                    m_HorizontalAngle = Input.GetAxis("Mouse X");

                    //Making sure the rotation angle stays between 0 and 360
                    m_HorizontalAngle = (m_HorizontalAngle > 360) ? m_HorizontalAngle - 360 : (m_HorizontalAngle < 360) ? m_HorizontalAngle + 360 : m_HorizontalAngle;

                    Vector3 fromTarget = transform.position - m_Target.position;
                    Vector3 nextVec = Quaternion.AngleAxis(m_HorizontalAngle, m_Target.up) * fromTarget;
                    Vector3 nextPos = m_Target.position + (nextVec.normalized * Mathf.Sqrt((keepDistanceAbove * keepDistanceAbove) + (m_KeepDistanceBehind * m_KeepDistanceBehind)));

                    transform.position = Vector3.MoveTowards(transform.position, nextPos, m_HorizontalAngle * camSensitivity * Time.deltaTime);
                }

                //if (Input.GetAxis("Mouse Y") != 0)
                //{
                //    float angle = Input.GetAxis("Mouse Y");

                //    if (angle < 0)
                //    {
                //        angle += 360;
                //    }
                //    else if (angle > 360)
                //    {
                //        angle -= 360;
                //    }

                //    float angleToCorrectAxis = ((Vector3.Dot(m_Target.right, Vector3.right)) / (m_Target.right.magnitude * Vector3.right.magnitude));

                //    Vector3 playerHorizontalAxis = Quaternion.AngleAxis(-angleToCorrectAxis, m_Target.up) * m_Target.right;

                //    Vector3 fromTarget = transform.position - m_Target.position;

                //    Vector3 nextVec = Quaternion.AngleAxis(angle, playerHorizontalAxis) * fromTarget;

                //    Vector3 nextPos = m_Target.position + (nextVec.normalized * Mathf.Sqrt((keepDistanceAbove * keepDistanceAbove) + (keepDistBehind * keepDistBehind)));


                //    transform.position = Vector3.MoveTowards(transform.position, nextPos, angle * camSensitivity * Time.deltaTime);
                //    transform.rotation = Quaternion.LookRotation((m_Target.position - transform.position).normalized);

                //}
                #endregion

            }

            #region Auto Cam

            transform.rotation = Quaternion.LookRotation((m_Target.position - transform.position).normalized);

            Vector3 playerToCam = transform.position - m_Target.position;

            //Camera doesn't move on Y axis so working on y plane to prevent changing the Y position
            Vector3 yPlanePlayerToCam = Vector3.ProjectOnPlane(playerToCam, Vector3.up);

            //Calculating the next position for the camera on the Y plane using player to camera direction and desired distance to keep behind the player
            Vector3 yPlaneNewCamPos = Vector3.ProjectOnPlane(m_Target.position, Vector3.up) + (yPlanePlayerToCam.normalized * m_KeepDistanceBehind);

            //Getting new position for camera by changing y value to cameras original y value
            Vector3 newCamPos = new Vector3(yPlaneNewCamPos.x, transform.position.y, yPlaneNewCamPos.z);




            RaycastHit camHit;

            if (Physics.Linecast(m_Target.position, transform.position, out camHit))
            {
                Vector3 vecToHit = camHit.point - m_Target.position;
                float distance = vecToHit.magnitude * 0.75f;
                newCamPos = m_Target.position + (vecToHit.normalized) * distance;
            }



            transform.position = Vector3.SmoothDamp(transform.position, newCamPos, ref m_Velocity, m_CamSpeed * Time.deltaTime);

            #endregion
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
        
    }

}
