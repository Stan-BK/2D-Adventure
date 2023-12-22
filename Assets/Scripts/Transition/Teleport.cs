using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour, IInteractive
{
    public GameSceneSO nextScene;
    public Vector3 posToGO;

    public void TriggerAction()
    {
        loadScene();
    }

    void loadScene()
    {
        LoadSceneSO.Instance.RaiseLoadSceneEvent(nextScene, posToGO, true);
    }
}
