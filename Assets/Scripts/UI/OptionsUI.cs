using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{

    public static OptionsUI Instance { get; private set; }

    private Action onCloseButtonAction;

    // ---Sound settings---
    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;

    // ---Bindings buttons---
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button altInteractButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button gamePadInteractButton;
    [SerializeField] private Button gamePadAltInteractButton;
    [SerializeField] private Button gamePadPauseButton;

    // ---Bindings text---
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI altInteractText;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private TextMeshProUGUI gamePadInteractText;
    [SerializeField] private TextMeshProUGUI gamePadAltInteractText;
    [SerializeField] private TextMeshProUGUI gamePadPauseText;

    // ---Rebind overlay---
    [SerializeField] private Transform pressToRebindKeyTransform;


    private void Awake()
    {
        Instance = this;

        soundEffectsButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        closeButton.onClick.AddListener(() =>
        {
            Hide();
            onCloseButtonAction();
        });

        // ---Rebind keys---
        moveUpButton.onClick.AddListener(() => {RebindKey(GameInput.Bindings.Move_Up);});
        moveDownButton.onClick.AddListener(() => {RebindKey(GameInput.Bindings.Move_Down);});
        moveLeftButton.onClick.AddListener(() => {RebindKey(GameInput.Bindings.Move_Left);});
        moveRightButton.onClick.AddListener(() => {RebindKey(GameInput.Bindings.Move_Right);});
        interactButton.onClick.AddListener(() => { RebindKey(GameInput.Bindings.Interact); });
        altInteractButton.onClick.AddListener(() => { RebindKey(GameInput.Bindings.InteractAlternate); });
        pauseButton.onClick.AddListener(() => { RebindKey(GameInput.Bindings.Pause); });
        gamePadInteractButton.onClick.AddListener(() => { RebindKey(GameInput.Bindings.Gamepad_Interact); });
        gamePadAltInteractButton.onClick.AddListener(() => { RebindKey(GameInput.Bindings.Gamepad_InteractAlternate); });
        gamePadPauseButton.onClick.AddListener(() => { RebindKey(GameInput.Bindings.Gamepad_Pause); });
    }

    private void Start()
    {
        GameStateManager.Instace.OnGameUnpaused += GameStateManager_OnGameUnpaused;

        UpdateVisual();

        HideRebindOverlay();
        Hide();
    }

    private void GameStateManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        // ---Update volume button text---
        soundEffectsText.text =  "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text =  "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);

        // ---Update keybind button text---
        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.Move_Up);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.Move_Right);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.Interact);
        altInteractText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.InteractAlternate);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.Pause);
        gamePadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.Gamepad_Interact);
        gamePadAltInteractText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.Gamepad_InteractAlternate);
        gamePadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.Gamepad_Pause);
    }

    public void Show(Action onCloseButtonAction)
    {
        this.onCloseButtonAction = onCloseButtonAction;

        gameObject.SetActive(true);

        soundEffectsButton.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    public void ShowRebindOverlay()
    {
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }

    private void HideRebindOverlay()
    {
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }

    private void RebindKey(GameInput.Bindings key)
    {
        ShowRebindOverlay();

        GameInput.Instance.RebindButton(key, () =>
        {
            HideRebindOverlay();
            UpdateVisual();
        });
    }

}
