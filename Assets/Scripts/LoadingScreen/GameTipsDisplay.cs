using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTipsDisplay : MonoBehaviour
{
    [SerializeField] private Text m_Tip;
    private List<string> m_TipList;

    // Start is called before the first frame update
    void Start()
    {
        m_TipList = new List<string>();

        m_TipList.Add("The Quicker you finish the\n higher your clear rank will be");
        m_TipList.Add("Your stats could determine your\n clear time so choose them wisely");

        int TipToDisplay = Random.Range(0, m_TipList.Count);

        m_Tip.text = m_TipList[TipToDisplay];
    }

   
}
