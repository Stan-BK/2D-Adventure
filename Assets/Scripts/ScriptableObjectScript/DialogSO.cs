using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialog")]
public class DialogSO : ScriptableObject
{
    [TextArea]public string text;
    public DialogSO children;
}
