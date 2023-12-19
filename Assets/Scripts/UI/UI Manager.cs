using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public CharacterEventSO characterEvent;
    public PlayerStateBar playerStateBar;
    public LoadedSceneSO loadedSceneSO;
    public Menu menuPanel;
    public Menu gameOverPanel;
    public PlayerInputControl InputControl;
    public AudioManager AudioManager;
    
    private Slider bgmSlider;
    private Slider fxSlider;
    private bool isMute;
    
    private void Awake()
    {
        InputControl = new PlayerInputControl();
    }

    private void OnEnable()
    {
        InputControl.Enable();
        InputControl.Player.Menu.started += OnShowMenu;
        
        characterEvent.OnHealthChange += OnHealthChange;
        characterEvent.OnSlideCdChange += OnSlideCdChange;
        loadedSceneSO.OnLoadedScene += OnLoadedScene;
    }

    private void OnShowMenu(InputAction.CallbackContext obj)
    {
        menuPanel.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void OnShowGameOver()
    {
        StartCoroutine(ShowGameOver());
    }

    private IEnumerator ShowGameOver()
    {
        yield return new WaitForSeconds(1);
        gameOverPanel.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    
    private void OnDisable()
    {
        InputControl.Disable();
        
        characterEvent.OnHealthChange -= OnHealthChange;
        characterEvent.OnSlideCdChange -= OnSlideCdChange;
        loadedSceneSO.OnLoadedScene -= OnLoadedScene;
    }

    private void OnLoadedScene(GameSceneSO arg0)
    {
        if (arg0.sceneType == SceneType.Menu)
        {
            playerStateBar.gameObject.SetActive(false);
        }
        else
        {
            playerStateBar.gameObject.SetActive(true);
        }
    }

    private void OnSlideCdChange(PlayerController playerController)
    {
        playerStateBar.cdChange(playerController.slideCD);
    }

    private void OnHealthChange(Character character)
    {
        float percentHealth = (float)character.currentHealth / character.maxHealth;
        playerStateBar.healthChange(percentHealth);
    }

    public void OnMuteGame()
    {
        isMute = !isMute;
        Debug.Log(isMute);
        AudioManager.ControlMute(isMute);
    }
    
    public void OnFXVolumeChange(GameObject slider)
    {
        if (!fxSlider)
        {
            fxSlider = slider.GetComponent<Slider>();
        }
        AudioManager.ChangeVolumn(VolumeType.FX, fxSlider.value);
    }

    public void OnBGMVolumeChange(GameObject slider)
    {
        if (!bgmSlider)
        {
            bgmSlider = slider.GetComponent<Slider>();
        }
        AudioManager.ChangeVolumn(VolumeType.BGM, bgmSlider.value);
    }
}
