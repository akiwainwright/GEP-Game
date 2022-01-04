using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PullLever : MonoBehaviour
{
    [SerializeField] private GameObject m_GameManager;
    [SerializeField] private GameObject m_Prompt;

    [SerializeField] private float m_RotateSpeed;

    private Quaternion m_NextRotation;
    private Quaternion m_StartRotation;

    private bool m_Triggered = false;
    private bool m_Rotating = false;

    private void Update()
    {
        if(m_Triggered && Input.GetKeyDown(KeyCode.F) && !m_Rotating)
        {
            m_Rotating = true;
            m_Prompt.SetActive(false);
        }

        if(m_Rotating)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, m_NextRotation, m_RotateSpeed * Time.deltaTime);

            if(transform.rotation == m_NextRotation)
            {
                m_Rotating = false;
                StartCoroutine(DelaySwitchDisappear());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !m_Triggered)
        {
            m_Prompt.SetActive(true);

            m_Triggered = true;

            m_StartRotation = transform.rotation;
            m_NextRotation = Quaternion.AngleAxis(90, Vector3.right) * m_StartRotation;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        m_Prompt.SetActive(false);
        if (!m_Rotating)
        {
            m_Triggered = false;
        }
    }

    IEnumerator DelaySwitchDisappear()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
        m_GameManager.GetComponent<GameManager>().gameStart = true;
    }
}
