using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{
    public float blinkSpeed = 0.3f;

    private bool m_FadeOut;
    private CanvasGroup m_CG;
   
    // Start is called before the first frame update
    void Start()
    {
        m_CG = GetComponent<CanvasGroup>();
        m_CG.alpha = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //only blinking if the blinking bok
        if (m_CG.enabled)
        {
            //if statments to determine whether to make press any button back ground fade in or out
            if (m_CG.alpha == 0f)
            {
                m_FadeOut = false;
            }
            if (m_CG.alpha == 1f)
            {
                m_FadeOut = true;
            }

            //Making backround of prompt text fade in or out by reducing alpha
            if (m_FadeOut)
            {
                m_CG.alpha -= blinkSpeed * Time.deltaTime;
            }
            else
            {
                m_CG.alpha += blinkSpeed * Time.deltaTime;
            }
        }
        
    }
}
