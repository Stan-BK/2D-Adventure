using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour, IInteractive
{
    public LoadSceneSO loadSceneSO;
    public SceneType sceneType;
    public GameSceneSO nextScene;
    public Vector3 posToGO;

    private void Awake()
    {
    }

    public void TriggerAction()
    {
        loadScene();
    }

    void loadScene()
    {
        loadSceneSO.RaiseLoadSceneEvent(nextScene, posToGO, true);
    }
}
