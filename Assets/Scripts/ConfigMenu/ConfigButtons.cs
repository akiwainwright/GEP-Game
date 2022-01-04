using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ConfigButtons : MonoBehaviour
{
    [SerializeField] private AudioSource m_SelectionSFX;

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer m_Mixer;

    [Header("Audio Sliders")]
    [SerializeField] private Slider m_MasterSldr;
    [SerializeField] private Slider m_BGMSldr;
    [SerializeField] private Slider m_SFXSldr;

    [Header("Interfaces")]
    [SerializeField] private GameObject m_AudioSettings;
    [SerializeField] private GameObject m_ConfigOptions;
    [SerializeField] private GameObject m_GraphicsSettings;
    [SerializeField] private GameObject m_Warining;

    [Header("Apply and Return Buttons")]
    [SerializeField] private Button m_BackToConfigAudio;
    [SerializeField] private Button m_BackToConfigGraphics;
    [SerializeField] private Button m_ApplyAudio;
    [SerializeField] private Button m_ApplyGraphics;

    [Header("Interface Titles")]
    [SerializeField] private Text m_ConfigText;
    [SerializeField] private Text m_AudioSettingText;
    [SerializeField] private Text m_GraphicsSettingText;


    public void Back()
    {
        m_SelectionSFX.Play();
        SceneManager.UnloadSceneAsync(2);
    }

    public void AudioSettingsUI()
    {
        m_ConfigText.gameObject.SetActive(false);
        m_ConfigOptions.SetActive(false);

        m_BackToConfigAudio.gameObject.SetActive(true);
        m_ApplyAudio.gameObject.SetActive(true);

        m_AudioSettingText.gameObject.SetActive(true);
        m_AudioSettings.SetActive(true);

        m_SelectionSFX.Play();
    }

    public void Graphics()
    {
        m_ConfigText.gameObject.SetActive(false);
        m_ConfigOptions.SetActive(false);

        m_BackToConfigGraphics.gameObject.SetActive(true);
        m_ApplyGraphics.gameObject.SetActive(true);

        m_GraphicsSettingText.gameObject.SetActive(true);
        m_GraphicsSettings.SetActive(true);

        m_SelectionSFX.Play();
    }

    public void WarningButtonNo()
    {
        if(m_AudioSettingText.gameObject.activeInHierarchy)
        {
            m_Warining.SetActive(false);
            m_AudioSettings.SetActive(true);
        }
        else
        {
            m_Warining.SetActive(false);
            m_GraphicsSettings.SetActive(true);
        }
    }

    public void WarningButtonYes()
    {
        m_Warining.SetActive(false);

        if (m_AudioSettingText.gameObject.activeInHierarchy)
        {
            m_Mixer.SetFloat("Master", PlayerPrefs.GetFloat("MasterVolume"));
            m_Mixer.SetFloat("BGM", PlayerPrefs.GetFloat("BGMVolume"));
            m_Mixer.SetFloat("SFX", PlayerPrefs.GetFloat("EffectVolume"));

            m_MasterSldr.value = PlayerPrefs.GetFloat("MasterVolume");
            m_BGMSldr.value = PlayerPrefs.GetFloat("BGMVolume");
            m_SFXSldr.value = PlayerPrefs.GetFloat("EffectVolume");

            m_AudioSettingText.gameObject.SetActive(false);
            m_BackToConfigAudio.gameObject.SetActive(false);
            m_ApplyAudio.gameObject.SetActive(false);
        }
        else
        {
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("GraphicsQuality"));

            m_GraphicsSettingText.gameObject.SetActive(false);
            m_ApplyGraphics.gameObject.SetActive(false);
            m_BackToConfigGraphics.gameObject.SetActive(false);
        }

        m_ConfigText.gameObject.SetActive(true);
        m_ConfigOptions.SetActive(true);
    }
}
