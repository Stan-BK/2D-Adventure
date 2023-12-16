using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/LoadedScene")]
public class LoadedSceneSO : ScriptableObject
{
    public UnityAction<GameSceneSO> OnLoadedScene;

    public void RaiseLoadedSceneEvent(GameSceneSO scene)
    {
        OnLoadedScene(scene);
    }
}
