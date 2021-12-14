using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }
    
    public void LoadConfig()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }

    public void QuitGame()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        QuitGame();
    }



}
