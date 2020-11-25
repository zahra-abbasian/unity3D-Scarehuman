using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float damagePoints = 100f;
    [SerializeField] ParticleSystem enemyExplosion;
    Animator enemyAnimator;
    PlayerHealth playerHealth;
    bool isDead;
    public int numDeadEnemies;
    

    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        enemyAnimator = GetComponent<Animator>();
    }

    public void ReceiveDamage(float damage)
    {
        BroadcastMessage("WhenShot");
        damagePoints -= damage;
        if (damagePoints <= 0)
        {
            EnemyDeath();
        }
    }

    public void EnemyDeath()
    {
        if (isDead)
        { return; }
        isDead = true;

        if (this.gameObject.tag == "Boss")
        {
            playerHealth.BossDead();
            enemyExplosion.Play();
            Destroy(gameObject, 6f);
        
        }
        else
        {
            numDeadEnemies++;
            playerHealth.numKilledEnemies++;
            enemyAnimator.SetTrigger("die");
            enemyExplosion.Play();
            Destroy(gameObject, 5f);
        }

    }

    public bool IsDead()
    {
        return isDead;
    }

    
}