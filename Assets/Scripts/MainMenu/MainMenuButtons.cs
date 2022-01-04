using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenuButtons : MonoBehaviour
{
    public static int SceneToLoad;

    [SerializeField] private AudioSource m_SelectionSFX;

    public void StartGame()
    {
        m_SelectionSFX.Play();
        SceneToLoad = 3;
        SceneManager.LoadScene(1);
    }
    
    public void LoadConfig()
    {
        m_SelectionSFX.Play();
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
    }

    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        //QuitGame();
    }



}
