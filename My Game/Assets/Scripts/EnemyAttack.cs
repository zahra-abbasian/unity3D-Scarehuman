// This script is inspired by the following tutorial:
// https://www.udemy.com/course/unitycourse2/learn/lecture/14893690#content

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    PlayerHealth player;
    [SerializeField] float damage = 10f;
    void Start()
    {
        player = FindObjectOfType<PlayerHealth>();
    }

    public void AttackHitEvent()
    {
        if (player == null) // If player is not found
        { return; }
       
        player.PlayerReceiveDamage(damage);
        player.GetComponent<DisplayDamage>().ShowDamage();
    }

}