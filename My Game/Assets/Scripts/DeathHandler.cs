using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
   [SerializeField] GameObject gameOverMenu;
   [SerializeField] GameObject pauseMenu;

   public void PlayerDeath()
   {
       pauseMenu.SetActive(false);
       gameOverMenu.SetActive(true);
       Time.timeScale = 0; // Stop time when dead
       Cursor.lockState = CursorLockMode.None; // Unluck the cursor
       Cursor.visible = true;
   }
}
