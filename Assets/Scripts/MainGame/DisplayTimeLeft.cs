using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayTimeLeft : MonoBehaviour
{

    [SerializeField] private Text m_TimeRemaining;

    private GameManager m_ManagerScript;

    private void OnEnable()
    {
        m_ManagerScript = GetComponent<GameManager>();

        if (m_ManagerScript.timeRemainingSeconds >= 0 && m_ManagerScript.timeRemainingSeconds <= 9)
        {
            m_TimeRemaining.text = m_ManagerScript.timeRemainingMins.ToString() + ":0" + m_ManagerScript.timeRemainingSeconds.ToString();
        }
        else
        {
            m_TimeRemaining.text = m_ManagerScript.timeRemainingMins.ToString() + ":" + m_ManagerScript.timeRemainingSeconds.ToString();
        }
    }

    private void Update()
    {
        if (m_ManagerScript.timeRemainingSeconds >= 0 && m_ManagerScript.timeRemainingSeconds <= 9)
        {
            m_TimeRemaining.text = m_ManagerScript.timeRemainingMins.ToString() + ":0" + m_ManagerScript.timeRemainingSeconds.ToString();
        }
        else
        {
            m_TimeRemaining.text = m_ManagerScript.timeRemainingMins.ToString() + ":" + m_ManagerScript.timeRemainingSeconds.ToString();
        }
    }
}
