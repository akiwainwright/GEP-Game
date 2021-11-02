using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreenEvents : MonoBehaviour
{
    public Text blinkingPromptText;
    public CanvasGroup blinkingPrompt;

    private GameObject blinkPanel;
    

    // Start is called before the first frame update
    void Start()
    {
        blinkPanel = GameObject.Find("BlinkingPanel");
    }

    // Update is called once per frame
    void Update()
    {
        if (blinkingPromptText.IsActive())
        {
            if(Input.anyKeyDown)
            {
                blinkingPrompt.enabled = false;
                blinkingPromptText.enabled = false;
                blinkPanel.SetActive(false);
            }
        }
    }
}
