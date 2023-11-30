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
    
    private float slideCd;
    private float percentHealth = 1;

    public void healthChange(float percentHealth)
    {
        healthImage.fillAmount = percentHealth;
    }

    public void cdChange(float cd)
    {
        powerImage.fillAmount = 0;
        slideCd = cd;
    }
    
    private void Update()
    {
        delayHealthImage();
        watchCdChange();
    }

    void delayHealthImage()
    {
        if (percentHealth <= healthImage.fillAmount) return;
        percentHealth -= 0.001f;
        healthDelayImage.fillAmount = percentHealth;
    }

    void watchCdChange()
    {
        if (powerImage.fillAmount >= 1) return;
        powerImage.fillAmount += Time.deltaTime;
    }
}
