using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;

public class AnyButtonFlashing : MonoBehaviour
{
    [SerializeField] private GameObject m_Panel;

    private Sequence m_FadeSequence = DOTween.Sequence();

    private CanvasGroup m_Background;

    [Header("Fade Speed")]
    [SerializeField] private float m_FadeSpeed = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        m_Background = m_Panel.GetComponent<CanvasGroup>();

        //Setting up fade animation
        m_FadeSequence.Append(m_Background.DOFade(0, m_FadeSpeed));
        m_FadeSequence.Append(m_Background.DOFade(1, m_FadeSpeed));

        //Setting animation to loop infinitely
        m_FadeSequence.SetLoops(-1);

        m_FadeSequence.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (m_Panel.transform.parent.gameObject.activeInHierarchy && !m_FadeSequence.IsPlaying())
        {
            m_FadeSequence.Restart();
        }
        else
        {
            m_FadeSequence.Pause();
        }
    }

   
}
