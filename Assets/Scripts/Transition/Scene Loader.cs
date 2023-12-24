using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public enum SceneType {
    Game,
    Menu
}

public class SceneLoader : MonoBehaviour
{
    public LoadSceneSO loadSceneSO;
    public LoadedSceneSO loadedSceneSO;
    public GameSceneSO firstLoadScene;
    public GameObject Player;
    public float fadeDuration;  
    public UnityEvent SetCameraBounds;
    public FadeControl fadeController;
    
    private GameSceneSO currentScene;
    private GameSceneSO nextScene;
    private Vector3 posToGo = Vector3.zero;
    private bool fadeScreen;

    void Awake()
    {
        nextScene = firstLoadScene;
        LoadScene();
        // Addressables.LoadSceneAsync(currentScene.sceneReference, LoadSceneMode.Additive);
        // currentScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
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
            fadeController.FadeIn();
            yield return new WaitForSeconds(fadeDuration);
        }
        
        yield return currentScene.sceneReference.UnLoadScene();

        LoadScene();
    } 

    public void LoadScene()
    {
        currentScene = nextScene;
        var loadSceneAsync =  currentScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
        loadSceneAsync.Completed += OnLoadSceneCompleted;
    }

    private void OnLoadSceneCompleted(AsyncOperationHandle<SceneInstance> obj)
    {
        if (fadeScreen)
        {
            fadeController.FadeOut();
        }
        SetCameraBounds?.Invoke();
        if (posToGo != Vector3.zero)
            Player.transform.position = posToGo;
        loadedSceneSO.RaiseLoadedSceneEvent(currentScene);
    }
}
