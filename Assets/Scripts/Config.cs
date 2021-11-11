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

    public void Start()
    {
        //Start with showing screen settings
        screenConfig.SetActive(true);
        cameraConfig.SetActive(false);
        volumeConfig.SetActive(false);

    }

    public void SetGraphicQuality(int quality)
    {
        
    }

    public void SetDisplayType(int type)
    {
        
    }

    public void SetResolution(int screenRes)
    {
              
    }

}
