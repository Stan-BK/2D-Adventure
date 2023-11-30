using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioEventSO attackAudioEventSO;
    public AudioEventSO bgmAudioEventSO;
    
    public AudioSource attackAudioSource;
    public AudioSource bgmAudioSource;
    
    private void OnEnable()
    {
        attackAudioEventSO.OnEventRaised += OnAudioTrigger;
        bgmAudioEventSO.OnEventRaised += OnBGMTrigger;
    }

    private void OnDisable()
    {
        attackAudioEventSO.OnEventRaised -= OnAudioTrigger;
        bgmAudioEventSO.OnEventRaised -= OnBGMTrigger;
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
