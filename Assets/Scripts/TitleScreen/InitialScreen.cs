using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialScreen : MonoBehaviour
{
    [Header("Screens")]
    public GameObject m_Title;
    public GameObject m_InitialTitlePrompt;
    public GameObject m_MainScreenSelections;
    public GameObject m_ConfigMenu;

    [Header("Default values")]
    public Dropdown defaultQuality;


    // Start is called before the first frame update
    void Awake()
    {
        //Setting first screen to display
        //Disabling all others to stop screens overlapping
        m_Title.SetActive(true);
        m_InitialTitlePrompt.SetActive(true);
        m_MainScreenSelections.SetActive(false);
        m_ConfigMenu.SetActive(false);

        defaultQuality.value = 2;
        Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_InitialTitlePrompt.activeInHierarchy)
        {
            if (Input.anyKeyDown)
            {
                m_InitialTitlePrompt.SetActive(false);
                m_MainScreenSelections.SetActive(true);
            }
        }
    }
}
