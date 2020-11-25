using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    
    [SerializeField] float healthAmount = 100f;
    [SerializeField] TextMeshProUGUI healthText;
    public int numKilledEnemies;
  

    void Update()
    {
        DisplayHealth();

    }

    public void PlayerReceiveDamage(float damage)
    {
        healthAmount -= damage;
        if (healthAmount <= 0)
        {
            healthAmount = 0;
            GetComponent<DeathHandler>().PlayerDeath();
        }
    }

    public void PlayerIncreaseHealth(int newHealthAmount)
    {
        healthAmount += newHealthAmount;
    }

    private void DisplayHealth()
    {
        int currentHealth = (int)healthAmount;
        healthText.text = currentHealth.ToString();
    }

    public void BossDead()
    {
        Invoke("LoadEndScene", 12f);
    }

     private void LoadEndScene()
    {
        SceneManager.LoadScene(2);
    }

}