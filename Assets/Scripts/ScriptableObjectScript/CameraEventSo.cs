using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(menuName = "Event/CameraEvent")]
public class CameraEventSo : ScriptableObject
{
    public UnityAction OnCameraShake;

    public void OnRaiseEvent()
    {
        OnCameraShake?.Invoke();
    }
}
