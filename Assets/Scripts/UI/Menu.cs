using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Menu : MonoBehaviour
{
    public GameSceneSO firstScene;
    public Vector3 posToGo;
    public UnityEvent ReplayGame;
    public LoadSceneSO loadSceneSO;
    public LoadedSceneSO loadedSceneSO;
    public bool isReload = false;

    private void OnEnable()
    {
        loadedSceneSO.OnLoadedScene += OnLoadedScene;
    }

    private void OnDisable()
    {
        loadedSceneSO.OnLoadedScene -= OnLoadedScene;
    }

    private void OnLoadedScene(GameSceneSO arg0)
    {
        if (isReload)
        {
            isReload = false;
            ReplayGame?.Invoke();
            gameObject.SetActive(false);
        }
    }

    public void ContinueToPlay()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void Replay()
    {
        if (isReload) return;
        isReload = true;
        loadSceneSO.RaiseLoadSceneEvent(firstScene, posToGo, true);
        Time.timeScale = 1; 
    }

    public void ExitGame()
    {
        
    }
}
