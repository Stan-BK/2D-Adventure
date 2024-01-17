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
    public AudioEventSO PlayerAudioEventSO;
    public AudioEventSO bgmAudioEventSO;
    public AudioEventSO openchestAudioEventSO;
    
    public AudioSource playerAudioSource;
    public AudioSource bgmAudioSource;
    public AudioSource openchestAudioSource;
    public AudioMixerSnapshot MuteSnapshot;
    public AudioMixerSnapshot UnmuteSnapshot;
    
    private void OnEnable()
    {
        PlayerAudioEventSO.OnEventRaised += OnAudioTrigger;
        bgmAudioEventSO.OnEventRaised += OnBGMTrigger;
        openchestAudioEventSO.OnEventRaised += OnOpenchestTrigger;

        PlayerAudioEventSO.OnPauseEventRaised += OnAudioPause;
    }

    private void OnDisable()
    {
        PlayerAudioEventSO.OnEventRaised -= OnAudioTrigger;
        bgmAudioEventSO.OnEventRaised -= OnBGMTrigger;
        openchestAudioEventSO.OnEventRaised -= OnOpenchestTrigger;
        
        PlayerAudioEventSO.OnPauseEventRaised -= OnAudioPause;
    }

    private void OnAudioPause()
    {
        playerAudioSource.Pause();
    }

    private void OnOpenchestTrigger(AudioClip audioClip, bool isLoop)
    {
        openchestAudioSource.clip = audioClip;
        openchestAudioSource.Play();
    }

    private void OnBGMTrigger(AudioClip audioClip, bool isLoop)
    {
        bgmAudioSource.clip = audioClip;
        bgmAudioSource.Play();
    }

    private void OnAudioTrigger(AudioClip audioClip, bool isLoop)
    {
        playerAudioSource.clip = audioClip;
        playerAudioSource.loop = isLoop;
        playerAudioSource.Play();
    }

    public void ChangeVolumn(VolumeType type, float val)
    {
        switch (type)
        {
            case VolumeType.FX:
            {
                playerAudioSource.volume = val;
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
