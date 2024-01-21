using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

enum DialogState
{
    First,
    FirstPlayEnd,
    HasShown
}
public class DialogManager : MonoBehaviour
{
    private DialogState dialogState = DialogState.First;
    private bool canClose = false;
    private TweenerCore<string, string, StringOptions> textTween;

    public PlayerInputControl InputControl;
    public GameObject DialogPanel;
    
    public DialogSO FirstInDialogSO;
    public DialogSO EndDialogSO;

    #region 事件函数
    
    private void Awake()
    {
        InputControl = new PlayerInputControl();
        InputControl.Player.Dialog.started += CloseDialog;
    }

    private void OnEnable()
    {
        InputControl.Enable();
    }

    private void OnDisable()
    {
        InputControl.Disable();
    }

    #endregion
    
    public void PopupDialog()
    {
        switch (dialogState)
        {
            case DialogState.First:
            {
                DialogPanel.SetActive(true);
                PlayerController.Instance.GetComponent<PlayerController>().InputControl.Disable();
                SetupTween(FirstInDialogSO.text);
                dialogState = DialogState.FirstPlayEnd;
            } break;
            case DialogState.FirstPlayEnd:
            {
                DialogPanel.SetActive(true);
                PlayerController.Instance.GetComponent<PlayerController>().InputControl.Disable();
                SetupTween(EndDialogSO.text);
                dialogState = DialogState.HasShown;
            } break;
            default:
            {
            } break;
        }
    }

    public void CloseDialog(InputAction.CallbackContext cbc)
    {
        if (!canClose)
        {
            canClose = true;
            textTween.timeScale = 999;
            return;
        }
        DialogPanel.SetActive(false);
        PlayerController.Instance.GetComponent<PlayerController>().InputControl.Enable();
        DialogPanel.GetComponentInChildren<Text>().text = "";
    }

    private void SetupTween(string text)
    {
        textTween = DialogPanel.GetComponentInChildren<Text>().DOText(text, 3f, true);
        textTween.SetEase(Ease.Linear);
        textTween.onComplete = () =>
        {
            canClose = true;
        };
    }
}
