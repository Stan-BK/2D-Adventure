using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public CharacterEventSO characterEvent;
    public PlayerStateBar playerStateBar;
    
    private void OnEnable()
    {
        characterEvent.OnHealthChange += OnHealthChange;
        characterEvent.OnSlideCdChange += OnSlideCdChange;
    }

    private void OnDisable()
    {
        characterEvent.OnHealthChange -= OnHealthChange;
        characterEvent.OnSlideCdChange -= OnSlideCdChange;
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
