using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StaminaBar : MonoBehaviour
{
    [SerializeField] private GameObject Player;

    private PlayerMovement m_PlayerScript;

    private Slider m_StaminaBar;

    private void Start()
    {
        m_StaminaBar = GetComponent<Slider>();
        m_PlayerScript = Player.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        Debug.Log("Stamina Left: " + m_PlayerScript.GetCurrentStamina());
        m_StaminaBar.value = m_PlayerScript.GetCurrentStamina() / m_PlayerScript.GetMaxStamina();
    }
}
