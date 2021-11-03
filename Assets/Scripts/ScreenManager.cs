using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    public GameObject titleScreen;
    public GameObject configScreen;

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Quit()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void SettingsScreen()
    {
        titleScreen.SetActive(false);
        configScreen.SetActive(true);
    }

    public void TitleScreen()
    {
        titleScreen.SetActive(true);
        configScreen.SetActive(false);
    }
}
