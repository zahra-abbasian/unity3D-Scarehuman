// This script is inspired by the following tutorial:
// https://www.youtube.com/watch?v=rXnZE8MwK-E

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreenController : MonoBehaviour
{
    public GameObject loadingScreenObject;
    public Slider slider;

    AsyncOperation async;
  
    public void StartLoadScreen()
    {
        StartCoroutine(LoadingScreen());
    }

    IEnumerator LoadingScreen()
    {
        loadingScreenObject.SetActive(true);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        async = SceneManager.LoadSceneAsync(currentSceneIndex + 1);
        async.allowSceneActivation = false;

        while (async.isDone == false)
        {
            slider.value = async.progress;
            if (async.progress == 0.9f)
            {
                slider.value = 1f;
                async.allowSceneActivation = true;
            } 
            yield return null;
        }
    }
   
}