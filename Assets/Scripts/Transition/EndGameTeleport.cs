using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameTeleport : MonoBehaviour
{
    public LoadSceneSO loadSceneSO;
    public GameSceneSO nextScene;
    public Vector3 posToGO;
    private void OnTriggerEnter2D(Collider2D other)
    {
        loadSceneSO.RaiseLoadSceneEvent(nextScene, posToGO, true);
    }
}
