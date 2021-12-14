using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PointerToOption : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private Image m_Pointer;

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_Pointer.rectTransform.anchorMin = new Vector2(m_Pointer.rectTransform.anchorMin.x, this.GetComponent<RectTransform>().anchorMin.y);
        m_Pointer.rectTransform.anchorMax = new Vector2(m_Pointer.rectTransform.anchorMax.x, this.GetComponent<RectTransform>().anchorMax.y);
    }    
    
}
