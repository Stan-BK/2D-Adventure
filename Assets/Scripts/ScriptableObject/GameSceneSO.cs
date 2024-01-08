using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "Event/GameScene")]
public class GameSceneSO: ScriptableObject
{
    public SceneName sceneName;
    public AssetReference sceneReference;
    public SceneType sceneType;
}
