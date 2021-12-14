using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfigButtons : MonoBehaviour
{
    public void Back()
    {
        SceneManager.UnloadSceneAsync(2);
    }
}
