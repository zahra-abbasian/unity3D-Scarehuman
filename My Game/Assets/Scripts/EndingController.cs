using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingController : MonoBehaviour
{
    [SerializeField] Canvas theEndCanvas;
    [SerializeField] Canvas creditsCanvas;
    
    void Start()
    {
        theEndCanvas.enabled = false;
        creditsCanvas.enabled = false;
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Boat")
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            StartCoroutine(DisplayEndCanvas());    
        }
    }

    private void DisplayCanvas(Canvas canvas)
    {
        canvas.enabled = true;
    }

    private void HideCanvas(Canvas canvas)
    {
        canvas.enabled = false;
    }

    IEnumerator DisplayEndCanvas()
    {
        DisplayCanvas(theEndCanvas);
        yield return new WaitForSecondsRealtime(3.0f);
        HideCanvas(theEndCanvas);
        yield return StartCoroutine(DisplayCreditsCanvas()); 
    }

    IEnumerator DisplayCreditsCanvas()
    {
        HideCanvas(theEndCanvas);
        DisplayCanvas(creditsCanvas);
        yield return new WaitForSecondsRealtime(10.0f);
        yield return StartCoroutine(DisplayMainMenu());
    }

    IEnumerator DisplayMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        yield return new WaitForSecondsRealtime(2.0f);
        yield return null;
    }
}