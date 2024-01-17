using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioEventSO audioEventSO;
    public bool playOnEnable;
    public bool isLoop;

    private void OnEnable()
    {
        if (playOnEnable)
        {
            playAudio();
        }
    }

    private void playAudio()
    {
        audioEventSO.RaiseEvent(audioClip, isLoop);
    }

    public void playAudio(AudioClip audioClip, bool isLoop)
    {
        audioEventSO.RaiseEvent(audioClip, isLoop);
    }

    public void pauseAudio()
    {
        audioEventSO.RaisePauseEvent();
    }
}
