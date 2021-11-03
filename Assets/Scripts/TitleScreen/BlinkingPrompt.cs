using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BlinkingPrompt : MonoBehaviour
{
    public float fadeSpeed = 1.5f;

    public GameObject m_Activator;

    [SerializeField][Tooltip("Used for easier contorl of alpha channel")]private CanvasGroup m_Iprompt;
    [SerializeField][Tooltip("Animations used for creating a blinking effect")]private Sequence m_FadeLoop;

    // Start is called before the first frame update
    void Start()
    {
        m_Iprompt = GetComponent<CanvasGroup>();
        m_Iprompt.alpha = 1f;

        m_FadeLoop = DOTween.Sequence();

        FadeTween();
        
    }

    //update is called once per frame
    void Update()
    {        
        //Stops playing the animation if the prompt is used and restarts the animation if the prompt is reactivated and the animation isn't already running
        if (!m_Activator.activeSelf)
        {
            m_FadeLoop.Pause();
        }
        //else if(m_Activator.activeSelf && !m_FadeLoop.IsActive())
        //{
        //    m_FadeLoop.Restart();
        //}
    }

    /// <summary>
    /// Sets up fade sequnce an infinte loop of the sequence
    /// </summary>
    void FadeTween()
    {
        Tween FadeOut = m_Iprompt.DOFade(0, fadeSpeed);
        Tween FadeIn = m_Iprompt.DOFade(1, fadeSpeed);

        m_FadeLoop.Append(FadeOut).Append(FadeIn);
        m_FadeLoop.SetLoops(-1);

        m_FadeLoop.Play();

    }
}
