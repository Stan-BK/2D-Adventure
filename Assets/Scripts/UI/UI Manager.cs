using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public CharacterEventSO characterEvent;
    public PlayerStateBar playerStateBar;
    public LoadedSceneSO loadedSceneSO;
    
    private void OnEnable()
    {
        characterEvent.OnHealthChange += OnHealthChange;
        characterEvent.OnSlideCdChange += OnSlideCdChange;
        loadedSceneSO.OnLoadedScene += OnLoadedScene;
    }

    private void OnDisable()
    {
        characterEvent.OnHealthChange -= OnHealthChange;
        characterEvent.OnSlideCdChange -= OnSlideCdChange;
        loadedSceneSO.OnLoadedScene -= OnLoadedScene;
    }

    private void OnLoadedScene(GameSceneSO arg0)
    {
        if (arg0.sceneType == SceneType.Menu)
        {
            playerStateBar.gameObject.SetActive(false);
        }
        else
        {
            playerStateBar.gameObject.SetActive(true);
        }
    }

    private void OnSlideCdChange(PlayerController playerController)
    {
        playerStateBar.cdChange(playerController.slideCD);
    }

    private void OnHealthChange(Character character)
    {
        float percentHealth = (float)character.currentHealth / character.maxHealth;
        playerStateBar.healthChange(percentHealth);
    }
}
