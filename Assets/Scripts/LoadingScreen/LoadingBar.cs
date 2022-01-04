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
     
        
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(MainMenuButtons.SceneToLoad);
        

        //AsyncOperation progess only displays loading values which is from 0 to 0.9
        //To get a value for the loading bar dividing the number by 0.9 will give a value between 0 and 1 which can be used for the slider

        //m_LoadBar.value = progressValue;

        //To get the percentage the progress value is just mulitplied by 100 as this is between 1 and 0
        //m_LoadPercent.text = (progressValue * 100f).ToString();

        while(!loadScene.isDone)
        {
            m_LoadBar.value = loadScene.progress / 0.9f;
            m_LoadPercent.text = ((loadScene.progress / 0.9f) * 100f).ToString();

            yield return null;
        }
       
    }
   
}
