using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioEventSO attackAudioEventSO;
    public AudioEventSO bgmAudioEventSO;
    public AudioEventSO openchestAudioEventSO;
    
    public AudioSource attackAudioSource;
    public AudioSource bgmAudioSource;
    public AudioSource openchestAudioSource;
    
    private void OnEnable()
    {
        attackAudioEventSO.OnEventRaised += OnAudioTrigger;
        bgmAudioEventSO.OnEventRaised += OnBGMTrigger;
        openchestAudioEventSO.OnEventRaised += OnOpenchestTrigger;
    }

    private void OnDisable()
    {
        attackAudioEventSO.OnEventRaised -= OnAudioTrigger;
        bgmAudioEventSO.OnEventRaised -= OnBGMTrigger;
        openchestAudioEventSO.OnEventRaised -= OnOpenchestTrigger;
    }

    private void OnOpenchestTrigger(AudioClip audioClip)
    {
        openchestAudioSource.clip = audioClip;
        openchestAudioSource.Play();
    }

    private void OnBGMTrigger(AudioClip audioClip)
    {
        bgmAudioSource.clip = audioClip;
        bgmAudioSource.Play();
    }

    private void OnAudioTrigger(AudioClip audioClip)
    {
        attackAudioSource.clip = audioClip;
        attackAudioSource.Play();
    }
}
