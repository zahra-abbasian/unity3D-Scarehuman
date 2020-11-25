// This script is inspired by the following tutorial:
// https://www.youtube.com/watch?v=9EJiEYUXQbE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Flashlight : MonoBehaviour
{
    [SerializeField] GameObject flashlight;
    [SerializeField] GameObject spotlight;
    [SerializeField] float maxEnergy;
    [SerializeField] AudioClip batteryPickupSound;
    [SerializeField] TextMeshProUGUI flashlightText;
    GameObject pickedupBattery;
    AudioSource audioSource;
    int batteries;
    int capacity = 60;
    float currentEnergy;
    float usedEnergy;
    bool flashlightEnabled;
    

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        maxEnergy = batteries * capacity;
        currentEnergy = maxEnergy;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Calculate maximum and current energy in each update
        maxEnergy = batteries * capacity;
        currentEnergy = maxEnergy;

        DisplayFlashlight();

        if (Input.GetKeyDown(KeyCode.F))
        {
            flashlightEnabled = !flashlightEnabled;
        }

        if (flashlightEnabled)
        {
            flashlight.SetActive(true);
            if (currentEnergy <= 0)
            {
                spotlight.SetActive(false);
                batteries = 0;
            }
            else if (currentEnergy > 0)
            {
                spotlight.SetActive(true); // Activate the light only when there is energy
                currentEnergy -= 1f * Time.deltaTime;
                usedEnergy += 1f * Time.deltaTime; 
            }

            if (usedEnergy >= capacity)
            {
                batteries--;
                usedEnergy = 0;
            }
        }
        else
        {
            flashlight.SetActive(false);
        }
        
    }

    public void OnTriggerEnter(Collider other) // Pick up battery and destroy it
    {
        if (other.gameObject.tag == "Battery")
        {
            pickedupBattery = other.gameObject;
            audioSource.PlayOneShot(batteryPickupSound);
            batteries++;
            Destroy(pickedupBattery);
        }
    }

    private void DisplayFlashlight()
    {
        flashlightText.text = batteries.ToString();
    }
}