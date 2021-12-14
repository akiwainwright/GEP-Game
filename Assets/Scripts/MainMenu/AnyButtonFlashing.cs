using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
using UnityEngine.Audio;

public class AnyButtonFlashing : MonoBehaviour
{
    [SerializeField] private GameObject m_PressAnyButton;
    [SerializeField] private GameObject m_MainScreenOptions;

    [SerializeField] private AudioSource m_ClickSFX;

    private Sequence m_FadeSequence;

    private CanvasGroup m_Background;

    [Header("Fade Speed")]
    [SerializeField] private float m_FadeSpeed = 1.5f;

    private bool m_CanPlaySFX;

    // Start is called before the first frame update
    void Start()
    {
        m_PressAnyButton.SetActive(true);
        m_MainScreenOptions.SetActive(false);

        m_Background = m_PressAnyButton.transform.GetChild(0).GetComponent<CanvasGroup>();

        //Setting up fade animation
        m_FadeSequence = DOTween.Sequence();

        Tween FadeOut = m_Background.DOFade(0, m_FadeSpeed);
        Tween FadeIn = m_Background.DOFade(1, m_FadeSpeed);

        m_FadeSequence.Append(FadeOut);
        m_FadeSequence.Append(FadeIn);
        m_FadeSequence.SetLoops(-1);

        //Setting animation to loop infinitely
        m_FadeSequence.SetLoops(-1);

        m_FadeSequence.Play();

        m_CanPlaySFX = true;
    }

    // Update is called once per frame
    void Update()
    {
     
        if(Input.anyKey)
        {
            if (m_CanPlaySFX)
            {
                m_ClickSFX.Play();
                m_CanPlaySFX = false;
            }
            
            m_FadeSequence.Pause();

            m_Background.transform.parent.gameObject.SetActive(false);
            m_MainScreenOptions.SetActive(true);
        }
        
        if(m_Background.transform.parent.gameObject.activeInHierarchy && !m_FadeSequence.IsPlaying())
        {
            m_FadeSequence.Restart();
        }
    }

   
}
