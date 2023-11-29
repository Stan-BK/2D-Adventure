using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public CharacterEventSO healthEvent;
    public PlayerStateBar playerStateBar;
    private void OnEnable()
    {
        healthEvent.OnHealthChange += OnHealthChange;
    }

    private void OnDisable()
    {
        healthEvent.OnHealthChange -= OnHealthChange;
    }
    
    private void OnHealthChange(Character character)
    {
        float percentHealth = (float)character.currentHealth / character.maxHealth;
        playerStateBar.healthChange(percentHealth);
    }
}
