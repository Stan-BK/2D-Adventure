using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "Event/GameScene")]
public class GameSceneSO: ScriptableObject
{
    public AssetReference sceneReference;
}
