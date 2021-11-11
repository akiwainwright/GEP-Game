using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
   
    [SerializeField] private Transform m_CameraFocusPoint;

    private Vector3 m_Velocity = Vector3.zero;

    [Header("Camera Movement")]
    public float keepDist = 6f;
    public float smooth = 0.3f;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        //Camera Initial Position
        transform.position = new Vector3(m_CameraFocusPoint.position.x, m_CameraFocusPoint.position.y + 2.5f, m_CameraFocusPoint.position.z - keepDist);

    }

    // Update is called once per frame
    void LateUpdate()
    {
        #region Auto Cam
        //AutoCam
        Vector3 camToFocusPoint = m_CameraFocusPoint.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(camToFocusPoint.normalized, Vector3.up);

        //Camera doesn't move on Y axis so working on y plane to prevent changing the Y position
        Vector3 m_CamPosYPlane = Vector3.ProjectOnPlane(transform.position, Vector3.up);
        Vector3 m_FocusPointYPlane = Vector3.ProjectOnPlane(m_CameraFocusPoint.position, Vector3.up);

        //Calculating the next position for the camera on the Y plane using player to camera direction and desired keep distance
        Vector3 yPlaneNewCamPos = m_FocusPointYPlane + ((m_CamPosYPlane - m_FocusPointYPlane).normalized * keepDist);

        //Getting new position for camera by changing y value to cameras original y value
        Vector3 newCamPos = new Vector3(yPlaneNewCamPos.x, transform.position.y, yPlaneNewCamPos.z);

        transform.position = Vector3.SmoothDamp(transform.position, newCamPos, ref m_Velocity, smooth * Time.deltaTime);
        #endregion
    }
}
