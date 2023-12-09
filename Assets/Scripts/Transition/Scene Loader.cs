using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public enum SceneType {
    Game,
    Menu
}

public class SceneLoader : MonoBehaviour
{
    public LoadSceneSO loadSceneSO;
    public GameSceneSO firstLoadScene;
    public GameObject Player;
    public float fadeDuration;

    private GameSceneSO currentScene;
    private GameSceneSO nextScene;
    private Vector3 posToGo;
    private bool fadeScreen;

    void Awake()
    {
        currentScene = firstLoadScene;
        currentScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
    }

    private void OnEnable()
    {
        loadSceneSO.OnLoadScene += OnLoadScene;
    }

    private void OnDisable()
    {
        loadSceneSO.OnLoadScene -= OnLoadScene;
    }

    private void OnLoadScene(GameSceneSO arg0, Vector3 arg1, bool arg2)
    {
        nextScene = arg0;
        posToGo = arg1;
        fadeScreen = arg2;

        StartCoroutine(UnLoadPreviousScene());
    }

    private IEnumerator UnLoadPreviousScene()
    {
        if (fadeScreen)
        {
            yield return new WaitForSeconds(fadeDuration);
        }

        yield return currentScene.sceneReference.UnLoadScene();
        
        LoadScene();
    } 

    public void LoadScene()
    {
        Addressables.LoadSceneAsync(nextScene.sceneReference, LoadSceneMode.Additive);
        Player.transform.position = posToGo;
    }
}
