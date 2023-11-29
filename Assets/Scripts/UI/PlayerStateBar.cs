using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerStateBar : MonoBehaviour
{
    public Image healthImage;
    public Image healthDelayImage;
    public Image powerImage;

    private float percentHealth = 1;

    public void healthChange(float percentHealth)
    {
        healthImage.fillAmount = percentHealth;
    }

    private void Update()
    {
        delayHealthImage();
    }

    void delayHealthImage()
    {
        if (percentHealth <= healthImage.fillAmount) return;
        percentHealth -= 0.001f;
        healthDelayImage.fillAmount = percentHealth;
    }
}
