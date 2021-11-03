using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitleAnimations : MonoBehaviour
{
    
    [Header("Press Any Button Animation")]
    public float fadeSpeed = 1.5f;
    public GameObject m_AnyButtonPrompt;
    public Image m_Prompt;
    private Sequence m_AnyButtonFade;
    

    // Start is called before the first frame update
    void Start()
    {
        #region PressAnyButtonPrompt
        m_AnyButtonFade = DOTween.Sequence();
        AnyButtonFadeSetUp();

        m_AnyButtonFade.Play();
        #endregion
    }

    // Update is called once per frame
    void Update()
    {

        #region StopBlinkingLoop
        if (!m_AnyButtonPrompt.activeInHierarchy)
        {
            m_AnyButtonFade.Pause();
        }
        #endregion
    }

    /// <summary>
    /// Setting up Fade Sequence for the initial press any button prompt
    /// </summary>
    #region AnyButtonFadeSequence
    void AnyButtonFadeSetUp()
    {
        Tween FadeOut = m_Prompt.DOFade(0, fadeSpeed);
        Tween FadeIn = m_Prompt.DOFade(0.5f, fadeSpeed);

        m_AnyButtonFade.Append(FadeOut).Append(FadeIn).SetLoops(-1);
    }
    #endregion
}
