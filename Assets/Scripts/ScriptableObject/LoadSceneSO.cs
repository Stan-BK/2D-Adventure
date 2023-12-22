using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/LoadScene")]
public class LoadSceneSO : ScriptableObject
{
    public UnityAction<GameSceneSO, Vector3, bool> OnLoadScene;
    public static LoadSceneSO Instance { get; set; } = null;

    private LoadSceneSO()
    {
        if (Instance) return;
        Instance = this;
    }

    public void RaiseLoadSceneEvent(GameSceneSO gameSceneSO, Vector3 posToGo, bool fadeScreen)
    {
        Instance.OnLoadScene(gameSceneSO, posToGo, fadeScreen);
    }
}
