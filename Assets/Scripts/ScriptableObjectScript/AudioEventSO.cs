using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/AudioEvent")]
public class AudioEventSO : ScriptableObject
{
    public UnityAction<AudioClip, bool> OnEventRaised;
    public UnityAction OnPauseEventRaised;

    public void RaiseEvent(AudioClip audioClip, bool isLoop)
    {
        OnEventRaised?.Invoke(audioClip, isLoop);
    }

    public void RaisePauseEvent()
    {
        OnPauseEventRaised?.Invoke();
    }
}
