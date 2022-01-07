using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject m_GameManager;

    private GameObject m_Enemy;

    private Rigidbody m_RB;
    private Animator m_Animator;

    [SerializeField] private AudioSource m_Kick;

    private bool m_CanAttack;
    private bool m_Hitting;

    

    // Start is called before the first frame update
    void Start()
    {
        m_RB = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();

        m_CanAttack = true;
        m_Hitting = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!m_GameManager.GetComponent<GameManager>().paused && (m_GameManager.GetComponent<GameManager>().playing || m_GameManager.GetComponent<GameManager>().preMatch))
        {
            //Attack input
            if (Input.GetKeyDown(KeyCode.Space) && m_CanAttack) 
            {
                m_Animator.SetBool("Attack", true);
                m_Animator.SetBool("Moving", false);
                m_Animator.SetBool("Sprint", false);
            }

            if(m_Animator.GetBool("Attack"))
            {
                m_RB.AddForce(transform.forward * GetComponent<CharacterStats>().power / 1.5f, ForceMode.Acceleration);
            }
            
            if(m_Enemy != null)
            {
                //Dealing damage to the enemy
                if (m_Hitting)
                {
                    m_Kick.Play();

                    //Stopping enemy from moving once hit
                    m_Enemy.GetComponent<NavMeshAgent>().isStopped = true;

                    //Changing enemy animation to defeated
                    m_Enemy.GetComponent<Animator>().SetBool("Defeated", true);
                    m_Enemy.GetComponent<Animator>().SetBool("Wandering", false);
                    m_Enemy.GetComponent<Animator>().SetBool("Flee", false);

                    m_Enemy = null;
                }
            }
            
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            m_Enemy = collision.gameObject;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        m_Enemy = null;
    }

    public void StopAttack()
    {
        m_Animator.SetBool("Attack", false);
        m_RB.velocity = Vector3.zero;
    }

    public void AttackAvailable()
    {
        m_CanAttack = true;
    }

    public void AggressiveFramesStart()
    {
        m_Hitting = true;
    }

    public void AggressivFramesEnd()
    {
        m_Hitting = false;
    }

    public void AttackUnAvailable()
    {
        m_CanAttack = false;
    }
}
