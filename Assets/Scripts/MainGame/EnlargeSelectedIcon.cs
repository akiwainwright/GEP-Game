using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EnlargeSelectedIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform m_Icon;

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_Icon = GetComponent<RectTransform>();

        m_Icon.anchorMax = new Vector2(m_Icon.anchorMax.x + 0.0325f, m_Icon.anchorMax.y);
        m_Icon.anchorMin = new Vector2(m_Icon.anchorMin.x , m_Icon.anchorMin.y - 0.04f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_Icon = GetComponent<RectTransform>();

        m_Icon.anchorMax = new Vector2(m_Icon.anchorMax.x - 0.0325f, m_Icon.anchorMax.y);
        m_Icon.anchorMin = new Vector2(m_Icon.anchorMin.x, m_Icon.anchorMin.y + 0.04f);
    }
}
