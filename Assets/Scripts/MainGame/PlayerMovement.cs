using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject m_GameManager;

    [SerializeField] private Transform m_Camera;

    private CharacterStats m_PlayerStats;

    private Rigidbody m_RB;
    private Animator m_Animator;

    private Vector3 moveDir;

    private float m_SpeedMultiplier;
    private float m_MaxStamina;
    private float m_CurrentStamina;
    public float staminaLossValue;
    public float staminaRecoverValue;

    private bool m_CanSprint;

    private void Start()
    {
        m_PlayerStats = GetComponent<CharacterStats>();

        m_SpeedMultiplier = 1f;

        m_MaxStamina = m_PlayerStats.stamina;
        m_CurrentStamina = m_MaxStamina;

        m_RB = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();

        m_CanSprint = true;
    }

    private void FixedUpdate()
    {
        if(m_CurrentStamina < 0)
        {
            m_CurrentStamina = 0;
            m_CanSprint = false;
        }

        //Making sure game isn't paused before player tries to move
        if (!m_GameManager.GetComponent<GameManager>().paused && (m_GameManager.GetComponent<GameManager>().playing || m_GameManager.GetComponent<GameManager>().preMatch))
        {
            if (!m_Animator.GetBool("Attack"))
            {
                m_Animator.speed = 1;

                #region Sprinting
                if (Input.GetKey(KeyCode.LeftShift) && m_CanSprint)
                {
                    //Setting up sprint animation, speed and reducing stamina while sprinting
                    m_Animator.SetBool("Sprint", true);

                    if (m_Animator.GetBool("Sprint") && m_Animator.GetBool("Moving"))
                    {
                        m_SpeedMultiplier = 1.4f;
                        m_CurrentStamina -= (staminaLossValue * Time.deltaTime);
                    }
                }
                else
                {
                    m_SpeedMultiplier = 1f;
                    m_Animator.SetBool("Sprint", false);
                    m_CurrentStamina += staminaRecoverValue * Time.deltaTime;

                    if (m_CurrentStamina > m_MaxStamina)
                    {
                        m_CurrentStamina = m_MaxStamina;
                    }

                    //Making player wait till there stamina recovers a bit before allowing them to sprint again
                    if (m_CurrentStamina > 30f && !Input.GetKey(KeyCode.LeftShift))
                    {
                        m_CanSprint = true;
                    }
                }
                #endregion

                #region Basic Player Movement
                if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 && !m_Animator.GetBool("Attack"))
                {
                    Vector3 horizontalDir = m_Camera.right * Input.GetAxis("Horizontal");

                    Vector3 verticalDir = Vector3.ProjectOnPlane(m_Camera.forward, Vector3.up) * Input.GetAxis("Vertical");

                    moveDir = (horizontalDir + verticalDir).normalized;

                    m_RB.velocity = moveDir * m_PlayerStats.speed * m_SpeedMultiplier * Time.deltaTime;
                    m_Animator.SetBool("Moving", true);
                }
                else
                {
                    m_Animator.SetBool("Moving", false);
                }

                if (moveDir != Vector3.zero)
                {
                    m_RB.rotation = Quaternion.LookRotation(moveDir, Vector3.up);
                }
                #endregion
            }
        }
        else
        {
            //pausing player animation when game is paused
            m_Animator.speed = 0;
        }
    }

    public float GetCurrentStamina()
    {
        return m_CurrentStamina;
    }

    public float GetMaxStamina()
    {
        return m_MaxStamina;
    }
}