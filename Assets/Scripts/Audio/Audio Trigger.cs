using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioEventSO audioEventSO;
    public bool playOnEnable;

    private void OnEnable()
    {
        if (playOnEnable)
        {
            playAudio();
        }
    }

    private void playAudio()
    {
        audioEventSO.RaiseEvent(audioClip);
    }
}
