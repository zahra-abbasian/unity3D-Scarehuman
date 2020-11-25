using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Pickups : MonoBehaviour
{
    [SerializeField] int bulletsAdded = 5;
    [SerializeField] int healthAdded = 30;
    [SerializeField] AudioClip bulletPickupSound;
    [SerializeField] AudioClip healthPickupSound;
    PlayerHealth playerHealth;
    Ammo ammo;
    GameObject pickedupBullet;
    GameObject pickedupHealth;
    AudioSource audioSource;
    int bulletAmount;
    int healthAmount;
    


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();   
    }

  
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            pickedupBullet = other.gameObject;
            audioSource.PlayOneShot(bulletPickupSound);
            ammo = FindObjectOfType<Ammo>();
            bulletAmount += bulletsAdded;
            ammo.IncreaseBulletAmount(bulletAmount);
            Destroy(pickedupBullet);
        }
        else if (other.gameObject.tag == "Health")
        {
            pickedupHealth = other.gameObject;
            audioSource.PlayOneShot(healthPickupSound);
            playerHealth = FindObjectOfType<PlayerHealth>();
            healthAmount += healthAdded;
            playerHealth.PlayerIncreaseHealth(healthAmount);
            Destroy(pickedupHealth);
        }
    }
}