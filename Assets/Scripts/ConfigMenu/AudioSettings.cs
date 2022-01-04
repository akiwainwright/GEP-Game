using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private AudioSource m_SelectionSFX;

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer m_GameSounds;

    [Header("Sound Setting Buttons")]
    [SerializeField] private Button m_NotMuted;
    [SerializeField] private Button m_Muted;
    [SerializeField] private Button m_BackToConfig;
    [SerializeField] private Button m_ApplyAudio;

    [Header("Interfaces")]
    [SerializeField] private GameObject m_AudioSettings;
    [SerializeField] private GameObject m_ConfigOptions;
    [SerializeField] private GameObject m_Warning;

    [Header("Interface Titles")]
    [SerializeField] private Text m_ConfigText;
    [SerializeField] private Text m_AudioSettingText;

    [Header("Audio Sliders")]
    [SerializeField] private Slider m_MasterVol;
    [SerializeField] private Slider m_BGMVol;
    [SerializeField] private Slider m_SFXVol;

    private bool m_SoundMuted = false;

    private void Start()
    {
        if (!m_SoundMuted)
        {
            m_NotMuted.gameObject.SetActive(true);
            m_Muted.gameObject.SetActive(false);
        }

        m_MasterVol.value = PlayerPrefs.GetFloat("MasterVolume");
        m_BGMVol.value = PlayerPrefs.GetFloat("BGMVolume");
        m_SFXVol.value = PlayerPrefs.GetFloat("EffectVolume");
    }

    public void Mute()
    {
        m_GameSounds.SetFloat("Master", -80f);
        m_NotMuted.gameObject.SetActive(false);
        m_Muted.gameObject.SetActive(true);
        m_SoundMuted = true;
    }

    public void UnMute()
    {
        m_GameSounds.SetFloat("Master", PlayerPrefs.GetFloat("MasterVolume"));
        m_NotMuted.gameObject.SetActive(true);
        m_Muted.gameObject.SetActive(false);
        m_SoundMuted = false;
    }

    public void SetMasterVol()
    {
        if (!m_SoundMuted)
        {
            m_GameSounds.SetFloat("Master", m_MasterVol.value);
        }
    }

    public void SetBGMVol()
    {
        if (!m_SoundMuted)
        {
            m_GameSounds.SetFloat("BGM", m_BGMVol.value);
        }
    }

    public void SetSFXVol()
    {
        if (!m_SoundMuted)
        {
            m_GameSounds.SetFloat("SFX", m_SFXVol.value);
        }
    }

    public void ApplyAudioSettings()
    {
        if (!m_Warning.activeInHierarchy)
        {
            //Master audio settings are affected by mute so if statement to prevent saving mute into playerprefs
            if (!m_SoundMuted)
            {
                PlayerPrefs.SetFloat("MasterVolume", m_MasterVol.value);
                m_GameSounds.SetFloat("Master", PlayerPrefs.GetFloat("MasterVolume"));
            }

            PlayerPrefs.SetFloat("BGMVolume", m_BGMVol.value);
            PlayerPrefs.SetFloat("EffectVolume", m_SFXVol.value);

            PlayerPrefs.Save();

            //Set Audio Values
            m_GameSounds.SetFloat("BGM", PlayerPrefs.GetFloat("BGMVolume"));
            m_GameSounds.SetFloat("SFX", PlayerPrefs.GetFloat("EffectVolume"));

            //Adjusting Audio Slider Values to Saved Values
            m_MasterVol.value = PlayerPrefs.GetFloat("MasterVolume");
            m_BGMVol.value = PlayerPrefs.GetFloat("BGMVolume");
            m_SFXVol.value = PlayerPrefs.GetFloat("EffectVolume");

            m_SelectionSFX.Play();
        }
    }

    public void ExitAudioSettings()
    {
        if (!m_Warning.activeInHierarchy)
        {
            float masterVol, BGMVol, SFXVol;

            m_GameSounds.GetFloat("Master", out masterVol);
            m_GameSounds.GetFloat("BGM", out BGMVol);
            m_GameSounds.GetFloat("SFX", out SFXVol);

            if (masterVol != PlayerPrefs.GetFloat("MasterVolume") || BGMVol != PlayerPrefs.GetFloat("BGMVolume") || SFXVol != PlayerPrefs.GetFloat("EffectVolume"))
            {
                m_Warning.SetActive(true);
                m_AudioSettings.SetActive(false);
            }
            else
            {
                m_BackToConfig.gameObject.SetActive(false);
                m_AudioSettings.SetActive(false);
                m_ConfigOptions.SetActive(true);
                m_AudioSettingText.gameObject.SetActive(false);
                m_ConfigText.gameObject.SetActive(true);
                m_ApplyAudio.gameObject.SetActive(false);

                m_MasterVol.value = PlayerPrefs.GetFloat("MasterVolume");
                m_BGMVol.value = PlayerPrefs.GetFloat("BGMVolume");
                m_SFXVol.value = PlayerPrefs.GetFloat("EffectVolume");
            }

            m_SelectionSFX.Play();
        }
    }

}
