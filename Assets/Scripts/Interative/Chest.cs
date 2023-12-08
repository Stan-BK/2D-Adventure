using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractive
{
    private SpriteRenderer spriteRenderer;
    public Sprite openedSprite;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TriggerAction()
    {
        spriteRenderer.sprite = openedSprite;
        gameObject.tag = "Untagged";
    }
}
