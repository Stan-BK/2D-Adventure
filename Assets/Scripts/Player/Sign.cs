using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sign : MonoBehaviour
{
    private PlayerInputControl InputControl;
    public GameObject signSprite;
    public Transform playerTransform;
    private bool canPress = false;
    private IInteractive target;

    private void Awake()
    {
        InputControl = new PlayerInputControl();
        InputControl.Player.Earn.started += OpenChest;
    }

    private void Update()
    {
        transform.localScale = playerTransform.localScale;
    }

    private void OpenChest(InputAction.CallbackContext obj)
    {
        if (canPress)
        {
            target.TriggerAction();
            canPress = false;
            signSprite.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Interactive"))
        {
            canPress = true;
            signSprite.SetActive(true);
            target = other.GetComponent<IInteractive>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Interactive"))
        {
            canPress = false;
            signSprite.SetActive(false);
            target = null;
        }
    }
    
    private void OnEnable()
    {
        InputControl.Enable();
    }

    private void OnDisable()
    {
        InputControl.Disable();
    }
}
