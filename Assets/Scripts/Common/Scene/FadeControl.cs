using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeControl : MonoBehaviour
{
    public Image fadeScreen;
    public float duration;
    public void FadeIn()
    {
        ControlFadeScreen(Color.black, duration);
    }

    public void FadeOut()
    {
        ControlFadeScreen(Color.clear, duration);
    }

    private void ControlFadeScreen(Color target, float duration)
    {
        fadeScreen.DOColor(target, duration);
    }
}
