using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defeated : MonoBehaviour
{
    [SerializeField] private ParticleSystem m_Smoke;
    [SerializeField] private GameObject m_GameManager;

    private void OnDestroy()
    {
        //making sure this only runs if game is playing to prevent error from quitting through pause
        //Called from animation event
        if (!m_GameManager.GetComponent<GameManager>().paused)
        {
            m_GameManager.GetComponent<GameManager>().EnemyDestroyed();
        }
    }

    public void EnemyDefeat()
    {
        m_Smoke.transform.position = transform.position;
        m_Smoke.Play();
    }
}
