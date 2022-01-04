using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsSettings : MonoBehaviour
{
    [SerializeField] private AudioSource m_SelectionSFX;

    [Header("Interfaces")]
    [SerializeField] private GameObject m_ConfigOptions;
    [SerializeField] private GameObject m_QualitySettings;
    [SerializeField] private GameObject m_Warning;

    [Header("Graphics Settings Buttons")]
    [SerializeField] private Button m_ApplyGraphics;
    [SerializeField] private Button m_BackToConfig;

    [Header("Interface Titles")]
    [SerializeField] private Text m_ConfigText;
    [SerializeField] private Text m_GraphicsText;

    private int m_QualityLevel;

    private void Start()
    {
        m_QualityLevel = PlayerPrefs.GetInt("GraphicsQuality");
    }

    public void SetGraphicsHigh()
    {
        m_QualityLevel = 2;
        
        m_SelectionSFX.Play();
    }

    public void SetGraphicsMedium()
    {
        m_QualityLevel = 1;

        m_SelectionSFX.Play();
    }

    public void SetGraphicsLow()
    {
        m_QualityLevel = 0;
        
        m_SelectionSFX.Play();
    }

    public void ApplyGraphicsSettings()
    {
        if (!m_Warning.activeInHierarchy)
        {
            PlayerPrefs.SetInt("GraphicsQuality", m_QualityLevel);
            QualitySettings.SetQualityLevel(m_QualityLevel);
            PlayerPrefs.Save();

            m_SelectionSFX.Play();
        }
    }

    public void BackToConfig()
    {
        if (!m_Warning.activeInHierarchy)
        {
            if (m_QualityLevel != PlayerPrefs.GetInt("GraphicsQuality"))
            {
                m_QualitySettings.SetActive(false);
                m_Warning.SetActive(true);
            }
            else
            {
                m_QualitySettings.SetActive(false);
                m_ConfigOptions.SetActive(true);

                m_BackToConfig.gameObject.SetActive(false);
                m_ApplyGraphics.gameObject.SetActive(false);

                m_GraphicsText.gameObject.SetActive(false);
                m_ConfigText.gameObject.SetActive(true);
            }

            m_SelectionSFX.Play();
        }
    }
}
