using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] int bulletAmount = 10;

    public int GetBulletAmount()
    {
        return bulletAmount;
    }

    public void DecreaseBulletAmount()
    {
        bulletAmount--;
    }

    public void IncreaseBulletAmount(int newBulletAmount)
    {
        bulletAmount += newBulletAmount;
    }
    
}