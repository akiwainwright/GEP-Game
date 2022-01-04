using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingStats : MonoBehaviour
{
    [SerializeField] private AudioSource m_Click;

    [SerializeField] private GameObject m_Player;
    [SerializeField] private GameObject m_PreMatchVisuals;

    [SerializeField] private Camera m_PreMatchCam;
    [SerializeField] private Camera m_ThirdPersonCam;

    [SerializeField] private Canvas m_PreMatchUI;
    [SerializeField] private Canvas m_GameUI;

    private CharacterStats m_StatsScript;
    [SerializeField] private Text m_PointsLeft;
    [SerializeField] private Text m_SpeedValue;
    [SerializeField] private Text m_StaminaValue;
    [SerializeField] private Text m_PowerValue;

    private int m_TotalPoints;

    private int m_Speed;
    private int m_Stamina;
    private int m_Power;

    private void Start()
    {
        m_StatsScript = m_Player.GetComponent<CharacterStats>();
        m_TotalPoints = 21;

        m_Speed = 0;
        m_Power = 0;
        m_Stamina = 0;

        m_SpeedValue.text = m_Speed.ToString();
        m_StaminaValue.text = m_Stamina.ToString();
        m_PowerValue.text = m_Power.ToString();

        m_PointsLeft.text = "Points Left: " + m_TotalPoints;
    }

    private void Update()
    {
         m_PointsLeft.text = "Points Left: " + m_TotalPoints;

        m_SpeedValue.text = m_Speed.ToString();
        m_StaminaValue.text = m_Stamina.ToString();
        m_PowerValue.text = m_Power.ToString();
    }

    public void SetAndStart()
    {
        m_Click.Play();
        m_StatsScript.speed = 150 + (m_Speed * 10);
        m_StatsScript.stamina = 150 + (m_Stamina * 10);
        m_StatsScript.power = 150 + (m_Power * 10);

        m_PreMatchCam.gameObject.SetActive(false);
        m_PreMatchUI.gameObject.SetActive(false);
        m_PreMatchVisuals.SetActive(false);

        m_ThirdPersonCam.gameObject.SetActive(true);
        m_Player.SetActive(true);
        m_GameUI.gameObject.SetActive(true);
    }

    public void IncreaseSpeed()
    {
        m_Click.Play();
        if(m_TotalPoints > 0 && m_Speed < 10)
        {
            m_Speed++;
            m_TotalPoints--;
        }
    }

    public void DecreaseSpeed()
    {
        m_Click.Play();
        if (m_Speed > 0)
        {
            m_Speed--;
            m_TotalPoints++;
        }
    }

    public void IncreaseStamina()
    {
        m_Click.Play();
        if (m_TotalPoints > 0 && m_Stamina < 10)
        {
            m_Stamina++;
            m_TotalPoints--;
        }
    }

    public void DecreaseStamina()
    {
        m_Click.Play();
        if (m_Stamina > 0)
        {
            m_Stamina--;
            m_TotalPoints++;
        }
    }

    public void IncreasePower()
    {
        m_Click.Play();
        if (m_TotalPoints > 0 && m_Power < 10)
        {
            m_Power++;
            m_TotalPoints--;
        }
    }

    public void DecreasePower()
    {
        m_Click.Play();
        if (m_Power > 0)
        {
            m_Power--;
            m_TotalPoints++;
        }
    }
}
