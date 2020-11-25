// This script is inspired by the following tutorial:
// https://www.udemy.com/course/unitycourse2/learn/lecture/14893690#content

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 30f;
    [SerializeField] GameObject hitEffect;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] AudioClip gunShotSound;
    AudioSource audioSource;

   void Start()
   {
       audioSource = GetComponent<AudioSource>();
   }
    void Update()
    {
        DisplayAmmo();
        if (Input.GetMouseButtonDown(0)) 
        {
            if (Time.timeScale != 0) // Prevents shooting when game is paused
            {
                Shoot();
            }
            
        }
        
    }

    private void DisplayAmmo()
    {
        int currentAmmo = ammoSlot.GetBulletAmount();
        ammoText.text = currentAmmo.ToString();
    }

    private void Shoot()
    {
        if (ammoSlot.GetBulletAmount() > 0)
        {
            audioSource.PlayOneShot(gunShotSound);
            ProcessRayCast();
            ammoSlot.DecreaseBulletAmount();
        }
        
    }

    private void ProcessRayCast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range)) // When hitting something
        {
            CreateHitImpact(hit);
            EnemyHealth enemy = hit.transform.GetComponent<EnemyHealth>();

            if (enemy == null) // If there is no EnemyHealth on an object
            { return; }

            enemy.ReceiveDamage(damage);
        }
        else // When hitting nothing
        {
            return;
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 0.1f); 
    }
}