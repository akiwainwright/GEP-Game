using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Config : MonoBehaviour
{
    [Header("Changable Setgings")]
    public GameObject screenConfig;
    public GameObject cameraConfig;
    public GameObject volumeConfig;

    public Dropdown windowType;
    public Dropdown resolutionOptions;

    Resolution[] screenResolutions;

    public void Start()
    {
        //Start with showing screen settings
        screenConfig.SetActive(true);
        cameraConfig.SetActive(false);
        volumeConfig.SetActive(false);

        screenResolutions = Screen.resolutions;

        resolutionOptions.ClearOptions();

        List<string> resolutionChoice = new List<string>();

        int currentRes = Screen.currentResolution.width * Screen.currentResolution.height;

        for (int i = 0; i < screenResolutions.Length; ++i)
        {
            int addedRes = screenResolutions[i].width * screenResolutions[i].height;

            string width = screenResolutions[i].width.ToString();
            string height = screenResolutions[i].height.ToString();

            resolutionChoice.Add(width + "x" + height);

            Debug.Log(resolutionChoice[i]);
            //resolutionChoice[i] = screenResolutions[i].width.ToString() + "x" + screenResolutions[i].height;

            if(currentRes == addedRes)
            {
                currentRes = i;
            }
        }

        resolutionOptions.AddOptions(resolutionChoice);
        resolutionOptions.value = currentRes;
        resolutionOptions.RefreshShownValue();
    }

    public void SetGraphicQuality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }

    public void SetDisplayType(int type)
    {
        if(type == 0)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        else if(type == 1)
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
        else if(type == 2)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
    }

    public void SetResolution(int screenRes)
    {
              Screen.SetResolution(screenResolutions[screenRes].width, screenResolutions[screenRes].height, Screen.fullScreen);
    }

}
