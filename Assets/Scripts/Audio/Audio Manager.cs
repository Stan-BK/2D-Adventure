using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum VolumeType
{
    BGM,
    FX
}

public class AudioManager : MonoBehaviour
{
    public AudioEventSO attackAudioEventSO;
    public AudioEventSO bgmAudioEventSO;
    public AudioEventSO openchestAudioEventSO;
    
    public AudioSource attackAudioSource;
    public AudioSource bgmAudioSource;
    public AudioSource openchestAudioSource;
    public AudioMixerSnapshot MuteSnapshot;
    public AudioMixerSnapshot UnmuteSnapshot;
    
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

    public void ChangeVolumn(VolumeType type, float val)
    {
        switch (type)
        {
            case VolumeType.FX:
            {
                attackAudioSource.volume = val;
                openchestAudioSource.volume = val;
            }
                break;
            case VolumeType.BGM:
            {
                bgmAudioSource.volume = val;
            } break;
        }
    }

    public void ControlMute(bool isMute)
    {
        if (isMute)
        {
            MuteSnapshot.TransitionTo(0);
        }
        else
        {
            UnmuteSnapshot.TransitionTo(0);
        }
    }
}
