using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MovePointerToOption : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject m_MenuOptions;

    private Image m_Pointer;

    private RectTransform[] m_Options;

    private Vector4[] m_ButtonCollider;

    private Vector2[,] m_PossiblePositions;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer Entered");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Pointer Exited");
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Pointer = m_MenuOptions.transform.GetChild(0).GetComponent<Image>();

        m_Options = new RectTransform[m_MenuOptions.transform.childCount - 1];

        //Getting rect transform of available options to use the anchor positio to move pointer
        for (int i = 0; i < m_Options.Length; ++i)
        {
            m_Options[i] = m_MenuOptions.transform.GetChild(i + 1).GetComponent<RectTransform>();
        }

        m_PossiblePositions = new Vector2[m_Options.Length, 2];

        //Setting possible positions for the pointer to be by modifying the min and max y anchors
        for (int i = 0; i < m_PossiblePositions.GetLength(0); ++i)
        {
            m_PossiblePositions[i, 0] = new Vector2(m_Pointer.rectTransform.anchorMin.x, m_Options[i].anchorMin.y);
            m_PossiblePositions[i, 1] = new Vector2(m_Pointer.rectTransform.anchorMax.x, m_Options[i].anchorMax.y);
        }

        m_ButtonCollider = new Vector4[m_Options.Length];

        //creating a box for each button to detect if mouse is over them
        for (int i = 0; i < m_ButtonCollider.Length; ++i)
        {
            float right = m_Options[i].transform.position.x + m_Options[i].rect.width / 2f;
            float left = m_Options[i].transform.position.x - m_Options[i].rect.width / 2f;
            float top = m_Options[i].transform.position.y + m_Options[i].rect.height / 2f;
            float bottom = m_Options[i].transform.position.y - m_Options[i].rect.height / 2f;

            m_ButtonCollider[i] = new Vector4(right, left, top, bottom);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_MenuOptions.activeInHierarchy)
        {
            for (int i = 0; i < m_ButtonCollider.Length; ++i)
            {
                if (Input.mousePosition.x < m_ButtonCollider[i].x && Input.mousePosition.x > m_ButtonCollider[i].y &&
                   Input.mousePosition.y < m_ButtonCollider[i].z && Input.mousePosition.y > m_ButtonCollider[i].w)
                {
                    m_Pointer.rectTransform.anchorMin = m_PossiblePositions[i, 0];
                    m_Pointer.rectTransform.anchorMax = m_PossiblePositions[i, 1];
                }
            }
        }
    }
}
