﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(AudioSource))]
public class DoorFlicker : MonoBehaviour
{
    private Tilemap overlay;
    private bool on;
    private AudioClip torch_loop;
    private AudioClip torch_light;
    private AudioClip torch_extinguish;
    private AudioSource audioSource;
    
    void Start()
    {
        torch_loop = (AudioClip)Resources.Load("Sounds/effects/Torch/Torch_Loop_1.mp3");
        torch_light = (AudioClip)Resources.Load("Sounds/effects/Torch/Torch_Light.wav");
        torch_extinguish = (AudioClip)Resources.Load("Sounds/effects/Torch/Torch_Extinguish.mp3");
        audioSource = GetComponent<AudioSource>();
        on = false;
        overlay = GetComponent<Tilemap>();
    }

    //Set graphic alpha to 0
    //likely unnecessary, we'll just deactivate/reactivate object as needed
    public void LightOff()
    {
        on = false;
        var temp = overlay.color;
        temp.a = 0;
        overlay.color = temp;
        audioSource.loop = false;
        audioSource.clip = torch_extinguish;
        audioSource.Play();
    }

    public void LightOn() {
        StartCoroutine(WaitLight());
        StopCoroutine(WaitLight());
    }

    //pause .01 seconds, increase graphic alpha by 5%
    //loops until alpha is 100% and the door is "lit"
    //start flicker animation
    IEnumerator WaitLight()
    {
        yield return new WaitForSeconds(0.01f);
        Debug.Log("test WaitLight()");
        var temp = overlay.color;
        temp.a += .05f;
        overlay.color = temp;

        if(temp.a <= .95)
        {
            audioSource.loop = false;
            audioSource.clip = torch_light;
            audioSource.Play();
            LightOn();
        } else
        {
            on = true;
            Flicker();
        }
    }

    // will need to call flicker after reactivating a room.
    public void Flicker()
    {
        audioSource.clip = torch_loop;
        audioSource.loop = true;
        audioSource.volume = 0.3f;
        audioSource.Play();
        StartCoroutine(WaitFlicker());
        StopCoroutine(WaitFlicker());
    }

    //continues forever, unless the door light goes off
    //ex: when the door is closed, flip to on = false
    //randomly pick a value between 50% and 100% for alpha every .05 seconds
    IEnumerator WaitFlicker()
    {
        yield return new WaitForSeconds(0.05f);
        var temp = overlay.color;
        temp.a = Random.Range(.5f, 1);
        overlay.color = temp;

        if (on)
        {
            Flicker();
        }
    }
}
