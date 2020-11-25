// This script is inspired by the following tutorial:
// https://www.udemy.com/course/unitycourse2/learn/lecture/14893690#content

using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAI : MonoBehaviour
{

    
    [SerializeField] float chaseRange = 10f;
    [SerializeField] float turnSpeed = 5f;
    [SerializeField] AudioClip attackMusic;
    [SerializeField] GameObject backgroundMusic;
    AudioSource audioSource;
    Animator enemyAnimator;
    NavMeshAgent navMeshAgent;
    float distanceToPlayer = Mathf.Infinity;
    bool isProvoked = false;
    EnemyHealth enemyHealth;
    Transform player;
    
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();
        player = FindObjectOfType<PlayerHealth>().transform;
        audioSource = GetComponent<AudioSource>();
        enemyAnimator = GetComponent<Animator>();
        
        // Sometimes the enemy is not placed on the navmesh at the start
        NavMeshHit closestHit;
        if (NavMesh.SamplePosition (transform.position, out closestHit, 100f, NavMesh.AllAreas)) {
            navMeshAgent.Warp(closestHit.position);
            navMeshAgent.enabled = true;
        }
          
    }

    
    void Update()
    {
       
         if (enemyHealth.IsDead())
        {
            this.enabled = false;
            navMeshAgent.enabled = false;
            isProvoked = false;

            audioSource.Stop();
            backgroundMusic.SetActive(true);
            
        }
        else if (!enemyHealth.IsDead())
        {
            //Debug.Log("Not dead yet");
        }
       

        if (navMeshAgent.isOnNavMesh) {
            
             distanceToPlayer = Vector3.Distance(player.position, transform.position);
            if (isProvoked)
            {
                backgroundMusic.SetActive(false);
                if (!audioSource.isPlaying)
                {
                    audioSource.clip = attackMusic;
                    audioSource.Play();
                }
                
                EngageWithPlayer();

            }
            else if (distanceToPlayer <= chaseRange)
            {
                isProvoked = true;
           
            }
            else if (!isProvoked)
            {
                audioSource.Stop();

                if (!backgroundMusic.active)
                {
                    backgroundMusic.SetActive(true);
                }

            }
        }

       
    }

    public void WhenShot() // Provoke enemy when shot from far by player
    {
        isProvoked = true;
    }

    private void EngageWithPlayer()
    {
        
        FacePlayer();
        if (distanceToPlayer >= navMeshAgent.stoppingDistance)
        {
            ChasePlayer();
        }
        else if (distanceToPlayer <= navMeshAgent.stoppingDistance)
        {
            AttackPlayer();
        }
    }

    private void ChasePlayer()
    {
        enemyAnimator.SetBool("attack", false);
        enemyAnimator.SetTrigger("move");
        navMeshAgent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        enemyAnimator.SetBool("attack", true);
       
    }

    private void FacePlayer()
    {
        Vector3 playerDirection = (player.position - transform.position).normalized; // Returns a unit vector (magnitude of 1)
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(playerDirection.x, 0, playerDirection.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }
    void OnDrawGizmosSelected() // Only to show the chase range radius
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}