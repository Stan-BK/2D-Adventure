using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Chest : MonoBehaviour, IInteractive
{
    private SpriteRenderer spriteRenderer;
    public Sprite openedSprite;
    public UnityEvent OpenChestAudio;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TriggerAction()
    {
        spriteRenderer.sprite = openedSprite;
        gameObject.tag = "Untagged";
        OpenChestAudio?.Invoke();
    }
}
