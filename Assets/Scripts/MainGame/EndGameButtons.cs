using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameButtons : MonoBehaviour
{
    [SerializeField] private Canvas m_Credits;
    public void ReturnMainMenu()
    {
        MainMenuButtons.SceneToLoad = 0;
        SceneManager.LoadScene(1);
    }

    public void Credits()
    {
        m_Credits.gameObject.SetActive(true);
    }

    public void CreditsBack()
    {
        m_Credits.gameObject.SetActive(false);
    }
}
