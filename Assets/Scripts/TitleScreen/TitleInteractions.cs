using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TitleInteractions : MonoBehaviour
{
    public GameObject m_InitialTitlePrompt;
    public GameObject m_MainScreenSelections;

    // Start is called before the first frame update
    void Start()
    {
        m_InitialTitlePrompt.SetActive(true);
        m_MainScreenSelections.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(m_InitialTitlePrompt.activeInHierarchy)
        {
            if(Input.anyKeyDown)
            {
                m_InitialTitlePrompt.SetActive(false);
                m_MainScreenSelections.SetActive(true);
            }
        }
        else
        {
            Debug.Log("Pause");
        }
    }
}
