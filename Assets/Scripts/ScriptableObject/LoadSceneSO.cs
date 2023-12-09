using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/LoadScene")]
public class LoadSceneSO : ScriptableObject
{
    public UnityAction<GameSceneSO, Vector3, bool> OnLoadScene;

    public void RaiseLoadSceneEvent(GameSceneSO gameSceneSO, Vector3 posToGo, bool fadeScreen)
    {
        OnLoadScene(gameSceneSO, posToGo, fadeScreen);
    }
}
