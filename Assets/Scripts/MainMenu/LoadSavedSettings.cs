using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LoadSavedSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer m_GameSounds;

    // Start is called before the first frame update
    void Start()
    {
        m_GameSounds.SetFloat("Master", PlayerPrefs.GetFloat("MasterVolume"));
        m_GameSounds.SetFloat("BGM", PlayerPrefs.GetFloat("BGMVolume"));
        m_GameSounds.SetFloat("SFX", PlayerPrefs.GetFloat("EffectVolume"));

        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("GraphicsQuality"));
    }
}
