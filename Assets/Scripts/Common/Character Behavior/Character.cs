using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("角色属性")] 
    public int maxHealth;
    public int currentHealth;
    [Tooltip("无敌时间")]public float invulnerableDuration;
    [HideInInspector]public bool invulnerable = false;

    public UnityEvent<Transform> OnTakeDamage;
    public UnityEvent OnDie;
    public UnityEvent<Character> OnHealthChange;
    private void Start()
    {
        currentHealth = maxHealth;
        //OnHealthChange?.Invoke(this);
    }

    public void TakeDamage(Attack attacker)
    {
        if (invulnerable) return;
        currentHealth -= attacker.damage;
        if (currentHealth > 0)
        {
            OnTakeDamage?.Invoke(attacker.transform);
        }
        else
        {
            OnDie?.Invoke();
        }

        OnHealthChange?.Invoke(this);
        StartCoroutine(TriggerInvulnerable());
    }
    
    public void Resurrect()
    {
        currentHealth = maxHealth;
        OnHealthChange?.Invoke(this);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Bounds"))
        {
            currentHealth = 0;
            OnHealthChange?.Invoke(this);
            OnDie?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (currentHealth == 0) return;
        if (other.CompareTag("Barrier"))
        {
            currentHealth = 0;
            OnHealthChange?.Invoke(this);
            OnDie?.Invoke();
        }
    }

    IEnumerator TriggerInvulnerable()
    {
        invulnerable = true;
        yield return new WaitForSeconds(invulnerableDuration);
        invulnerable = false;
    }
}
