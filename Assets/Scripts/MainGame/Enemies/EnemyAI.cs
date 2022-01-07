using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private GameObject m_Player;

    private Animator m_Animator;

    private NavMeshAgent m_Agent;

    private Vector3 m_NextPosition;

    private bool m_InPossibleRange;
    private bool m_Running;

    void OnEnable()
    {
        m_InPossibleRange = false;

        m_NextPosition = transform.position;

        m_Agent = GetComponent<NavMeshAgent>();
        m_Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!m_Animator.GetBool("Defeated"))
        {
            if (m_InPossibleRange)
            {
                #region Field of View

                //Getting vector to player and checking angle offset between enemy and player
                Vector3 enemyToPlayer = m_Player.transform.position - transform.position;

                float angleBetween = Mathf.Acos((Vector3.Dot(enemyToPlayer, transform.forward)) / (enemyToPlayer.magnitude * transform.forward.magnitude)) * Mathf.Rad2Deg;

                if (angleBetween < 45)
                {
                    //If player is within 90 of the enemy facing direction player is within enemy field of view
                    RaycastHit obstacle;
                    if (Physics.Linecast(transform.position, m_Player.transform.position, out obstacle))
                    {
                        //making sure there are no objects onstructing view before confirming enemy can see player
                        if (obstacle.transform.tag == "Player")
                        {
                            Flee();
                        }
                    }


                }
                //If player gets too close and isn't in field of view triggers second flee condition
                else if (enemyToPlayer.magnitude < 3.75f)
                {
                    Flee();
                }
                else
                {
                    m_Running = false;
                }
                #endregion

            }
            else
            {
                if (m_Agent.remainingDistance == 0 && !m_Running)
                {
                    Wander();
                }
            }
        }



    }

    private void OnTriggerEnter(Collider other)
    {
        //Detecting player entered possible field of view
        if(other.tag == "Player")
        {
            m_InPossibleRange = true;
            

            m_Player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Detecting player left possible field of view
        if(other.tag == "Player")
        {
            m_Agent.stoppingDistance = 0;
            m_InPossibleRange = false;
            m_Running = false;
            Wander();
        }
    }

    private void Wander()
    {
        SetWanderPosition();

        m_Agent.speed = 2;

        m_Agent.SetDestination(m_NextPosition);
    }

    private void Flee()
    {
        m_Running = true;

        SetFleePosition();        

        m_Agent.speed = 5f;

        m_Agent.SetDestination(m_NextPosition);
    }

    private void SetWanderPosition()
    {
        //Setting up animation parameters for slow running
        m_Animator.SetBool("Wandering", true);
        m_Animator.SetBool("Flee", false);

        CheckNavMesPos(10f, 10f);
    }

    private void SetFleePosition()
    {
        //setting animation paramaters to run
        m_Animator.SetBool("Flee", true);

        CheckNavMesPos(10f, 20f);
        
    }

    private void CheckNavMesPos(float areaToGoTo, float nextPosRange)
    {
        //Determine position to go to
        float nextZpos = (Random.Range(-nextPosRange, nextPosRange) + transform.position.z);
        float nextXpos = (Random.Range(-nextPosRange, nextPosRange) + transform.position.x);

        if (m_Running)
        {
            float randomZ = Random.Range(-nextPosRange, nextPosRange);
            float randomX = Random.Range(-nextPosRange, nextPosRange);

            if(randomZ < 0 && randomZ >= -5f)
            {
                randomZ -= 10;
            }
            else if(randomZ >= 0 && randomZ <= 5)
            {
                randomZ += 10;
            }

            if (randomX < 0 && randomX >= -5f)
            {
                randomX -= 10;
            }
            else if (randomX >= 0 && randomX <= 5)
            {
                randomX += 10;
            }

            nextZpos = randomZ + transform.position.z;
            nextXpos = randomX + transform.position.x;

        }

        float clampedZpos = Mathf.Clamp(nextZpos, -40f, 40f);
        float clampedXpos = Mathf.Clamp(nextXpos, -40f, 40f);


        m_NextPosition = new Vector3(clampedXpos, 0, clampedZpos);

        NavMeshHit pointOnNavMesh;

        //Checking to see if position is on nav mesh if not getting nearest position that is
        if (NavMesh.SamplePosition(m_NextPosition, out pointOnNavMesh, areaToGoTo, NavMesh.AllAreas))
        {
            m_NextPosition = pointOnNavMesh.position + new Vector3(0, transform.position.y, 0);
        }
    }
}
