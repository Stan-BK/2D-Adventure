using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

static class Utils
{
    static public IEnumerator SetTimeout(float time, [CanBeNull] Action OnComplete)
    {
        yield return new WaitForSeconds(time);
        OnComplete?.Invoke();
    }
}
