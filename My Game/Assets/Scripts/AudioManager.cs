using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   
    [SerializeField] AudioClip backgroundTrack;
    AudioSource audioSource;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    void Update()
    {  
         if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

}
