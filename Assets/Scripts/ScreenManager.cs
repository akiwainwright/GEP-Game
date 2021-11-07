using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    public GameObject titleScreen;
    public GameObject configScreen;

    #region SceneLoading
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    #endregion

    #region QuitGame
    public void Quit()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }
    #endregion

    //Methods to switch between main and config screen
    #region ConfigScreen
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
    #endregion

}
