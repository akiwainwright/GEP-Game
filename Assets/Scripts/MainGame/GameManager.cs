using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas m_PausedScreen;
    [SerializeField] private Canvas m_GameUI;

    [SerializeField] private AudioSource m_GameBGM;
    [SerializeField] private AudioSource m_PreGameBGM;
    [SerializeField] private AudioSource m_Click;

    [SerializeField] private Text m_TimeLeft;
    [SerializeField] private Text m_EnemiesRemaining;

 

    public int timeRemainingMins;
    public int timeRemainingSeconds;

    public bool gameStart;
    public bool paused;

    // Start is called before the first frame update
    void Start()
    {
        paused = false;
        gameStart = false;
        m_GameUI.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                paused = true;
                m_PausedScreen.gameObject.SetActive(true);

                m_GameUI.gameObject.SetActive(false);
                
            }
            else
            {
                paused = false;
                m_PausedScreen.gameObject.SetActive(false);

                m_GameUI.gameObject.SetActive(true);
            }
        }

        if(gameStart)
        {
           
            m_GameBGM.Play();
            

            m_PreGameBGM.Stop();

            timeRemainingMins = 3;
            timeRemainingSeconds = 0;

            m_TimeLeft.gameObject.SetActive(true);
            m_EnemiesRemaining.gameObject.SetActive(true);
            

            StartCoroutine(Timer());

            gameStart = false;
        }
    }

    public void PauseGameButton()
    {
        m_Click.Play();
        paused = true;
        m_PausedScreen.gameObject.SetActive(true);

        m_GameUI.gameObject.SetActive(false);
    }

    public void ResumeGame()
    {
        m_Click.Play();
        paused = false;
        m_PausedScreen.gameObject.SetActive(false);

        m_GameUI.gameObject.SetActive(true);
    }

    public void PauseQuitButton()
    {
        m_Click.Play();
        MainMenuButtons.SceneToLoad = 0;

        SceneManager.LoadScene(1);
    }

    public void LoadConfig()
    {
        m_Click.Play();
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1);

        timeRemainingSeconds--;

        if (timeRemainingSeconds < 0)
        {
            timeRemainingMins--;

            if (timeRemainingMins > 0)
            {
                timeRemainingSeconds += 60;
            }
            else
            {
                timeRemainingSeconds = 0;
            }
        }

        if(timeRemainingMins > 0 && timeRemainingSeconds > 0)
        {
            StartCoroutine(Timer());
        }

    }
}
