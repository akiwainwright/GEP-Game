using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    [SerializeField] private Slider m_LoadBar;
    [SerializeField] private Text m_LoadPercent;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {        
        //load scene in background
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(MainMenuButtons.SceneToLoad);

        while(!loadScene.isDone)
        {
            //getting progress as a percentage
            m_LoadBar.value = loadScene.progress / 0.9f;
            m_LoadPercent.text = ((loadScene.progress / 0.9f) * 100f).ToString();

            yield return new WaitForSeconds(5f);
        }
       
    }
   
}
