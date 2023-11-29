using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(menuName = "Event/CharacterObject")]
public class CharacterEventSO : ScriptableObject
{
    public UnityAction<Character> OnHealthChange;

    public void ChangeHealth(Character character)
    {
        OnHealthChange?.Invoke(character);
    }
}
