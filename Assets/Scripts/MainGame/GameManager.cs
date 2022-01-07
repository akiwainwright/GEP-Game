using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject m_Enemies;
    [SerializeField] private GameObject m_Walls;
    [SerializeField] private GameObject m_MainGame;
    [SerializeField] private GameObject m_Player;
    [SerializeField] private GameObject m_Win;
    [SerializeField] private GameObject m_Controls;

    [SerializeField] private Camera m_Camera;

    [SerializeField] private Canvas m_PausedScreen;
    [SerializeField] private Canvas m_GameUI;
    [SerializeField] private Canvas m_GameOver;

    [SerializeField] private AudioSource m_GameBGM;
    [SerializeField] private AudioSource m_PreGameBGM;
    [SerializeField] private AudioSource m_Click;
    [SerializeField] private AudioSource m_GameOverSound;

    [SerializeField] private Text m_TimeLeft;
    [SerializeField] private Text m_EnemiesRemaining;
    [SerializeField] private Text m_YourScore;

    [SerializeField] private Text Top1Time;
    [SerializeField] private Text Top2Time;
    [SerializeField] private Text Top3Time;

    private int m_EnemiesLeft;

    public int timeRemainingMins;
    public int timeRemainingSeconds;

    public bool gameStart;
    public bool playing;
    public bool paused;

    private bool m_GameWon;
    private bool m_WinScreenSet;
    public bool lost;
    public bool preMatch;

    private bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        paused = false;
        gameStart = false;
        playing = false;
        preMatch = true;
        m_GameUI.gameObject.SetActive(false);

        m_EnemiesLeft = m_Enemies.transform.childCount;

        m_Controls.SetActive(true);

        m_GameWon = false;
        m_WinScreenSet = false;
        lost = false;

        gameOver = false;

        timeRemainingMins = 2;
        timeRemainingSeconds = 30;
    }

    private void Update()
    {
        //Pre Match before lever pulled
        if (preMatch)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                KeyboardPause();
            }

            if (gameStart && !playing)
            {
                #region starting main game 
                m_GameBGM.Play();


                m_PreGameBGM.Stop();

                m_TimeLeft.gameObject.SetActive(true);
                m_EnemiesRemaining.text = "Enemies Left: " + m_EnemiesLeft;
                m_EnemiesRemaining.gameObject.SetActive(true);

                m_Enemies.SetActive(true);
                m_Walls.SetActive(true);
                m_Controls.SetActive(false);

                gameStart = false;

                playing = true;
                preMatch = false;

                StartCoroutine(Timer());
                #endregion
            }
        }

        //Lever pulled
        if(playing && !gameStart)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                KeyboardPause();
            }

            if (m_EnemiesLeft == 0 && timeRemainingMins > -1)
            {
                playing = false;
                m_GameWon = true;
            }

        }
        //WinCondiditon
        else if (!playing && m_GameWon && !m_WinScreenSet)
        {
            m_WinScreenSet = true;

            #region high scores

            //Calculate total seconds remaining to store in player prefs

            int totalSecondsRemaining = (timeRemainingMins * 60) + timeRemainingSeconds;

            if (PlayerPrefs.HasKey("Top"))
            {
                #region setting high scores

                //Getting current top scores from player prefs
                int best = PlayerPrefs.GetInt("Top");
                int second = PlayerPrefs.GetInt("Second");
                int third = PlayerPrefs.GetInt("Third");

                if(totalSecondsRemaining > best)
                {
                    PlayerPrefs.SetInt("Top", totalSecondsRemaining);
                    PlayerPrefs.SetInt("Second", best);
                    PlayerPrefs.SetInt("Third", second);
                }
                else if(totalSecondsRemaining > second)
                {
                    PlayerPrefs.SetInt("Second", totalSecondsRemaining);
                    PlayerPrefs.SetInt("Third", second);
                }
                else if(totalSecondsRemaining > third)
                {
                    PlayerPrefs.SetInt("Third", totalSecondsRemaining);
                }
                #endregion 
            }

            else
            {
                //if no highscores setting the current score as the high score and remaining two to maximum time
                PlayerPrefs.SetInt("Top", totalSecondsRemaining);
                PlayerPrefs.SetInt("Second", 240);
                PlayerPrefs.SetInt("Third", 240);
            }

            SetScores(Top1Time, PlayerPrefs.GetInt("Top"));
            SetScores(Top2Time, PlayerPrefs.GetInt("Second"));
            SetScores(Top3Time, PlayerPrefs.GetInt("Third"));
            #endregion

            StopAllCoroutines();

            StartCoroutine(DelayWinScreen());
        }

        //Lose Condition
        else if(lost && !gameOver)
        {

            StopAllCoroutines();

            Lose();
        }
        
    }

    public void PauseGameButton()
    {
        m_Click.Play();
        paused = true;
        m_PausedScreen.gameObject.SetActive(true);

        m_GameUI.gameObject.SetActive(false);
    }

    private void KeyboardPause()
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
        //Coroutine to act as a timer for the game;
        while (playing)
        {
            yield return new WaitForSeconds(1);

            if (!paused)
            {
                //Making sure that there is still time remaining for the player to win
                if (timeRemainingMins > -1)
                {
                    timeRemainingSeconds--;

                    if (timeRemainingSeconds < 0)
                    {
                        timeRemainingSeconds += 60;
                        timeRemainingMins--;

                        if (timeRemainingMins < 0)
                        {
                            //once time remaining hits 0 triggering loss
                            timeRemainingMins = 0;
                            timeRemainingSeconds = 0;
                            lost = true;
                            playing = false;
                        }
                    }
                }
            }
        }

        yield return new WaitForEndOfFrame();
    }

    IEnumerator DelayWinScreen()
    {
        yield return new WaitForSeconds(1);

        #region setting up Win screen
        m_MainGame.SetActive(false);
        m_Camera.gameObject.SetActive(false);
        m_Player.SetActive(false);
        m_GameUI.gameObject.SetActive(false);


        m_Win.SetActive(true);

        if (timeRemainingSeconds < 10)
        {
            m_YourScore.text = timeRemainingMins.ToString() + ":0" + timeRemainingSeconds.ToString();
        }
        else
        {
            m_YourScore.text = timeRemainingMins.ToString() + ":" + timeRemainingSeconds.ToString();
        }

        Cursor.lockState = CursorLockMode.None;

        m_PreGameBGM.Play();

        m_GameBGM.Stop();
        #endregion
    }

    IEnumerator DelayedGameOver()
    {
        yield return new WaitForSeconds(1);

        #region Game Over screen
        m_GameOver.gameObject.SetActive(true);

        Cursor.lockState = CursorLockMode.None;

        m_MainGame.SetActive(false);
        m_Player.SetActive(false);
        m_GameUI.gameObject.SetActive(false);
        #endregion
    }


    public void EnemyDestroyed()
    {
        if (!paused)
        {
            m_EnemiesLeft--;
            m_EnemiesRemaining.text = "Enemies Left: " + m_EnemiesLeft;
        }
    }

    public void Lose()
    {
        StopAllCoroutines();

        m_GameBGM.Stop();

        m_GameOverSound.Play();


        lost = false;

        StartCoroutine(DelayedGameOver());
    }

    public void SetScores(Text text, int score)
    {
        int seconds = score % 60;
        int mins = (score - seconds) / 60;

        if (seconds < 10)
        {
            text.text = mins + ":0" + seconds;
        }
        else
        {
            text.text = mins + ":" + seconds;
        }
        
    }
}
