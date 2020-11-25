using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameOverMenu;
    bool isPaused;

    private void Start()
    {
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }
        
        if (isPaused)
        {
            ActivatePauseMenu();
        }
        else
        {
            if (gameOverMenu.active)
            {
                Time.timeScale = 0f;
            }
            else
            {
                DeactivatePauseMenu();
            }
            
        }
    }

    void ActivatePauseMenu()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    public void DeactivatePauseMenu()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    public void ActivateMainMenu()
   {
       Time.timeScale = 1; 
       SceneManager.LoadScene(0);
   }

   public void ReloadGame()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene(0);
        
    }

   public void QuitGame()
   {
       Application.Quit();
   }

  
}