using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfigButtons : MonoBehaviour
{
    [SerializeField] private AudioSource m_SelectionSFX;

    public void Back()
    {
        m_SelectionSFX.Play();
        SceneManager.UnloadSceneAsync(1);
    }
}
