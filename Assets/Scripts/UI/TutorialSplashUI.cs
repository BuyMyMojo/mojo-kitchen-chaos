using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialSplashUI : MonoBehaviour
{

    // ---Movement---
    [SerializeField] private TextMeshProUGUI keyMoveUpText;
    [SerializeField] private TextMeshProUGUI keyMoveDownText;
    [SerializeField] private TextMeshProUGUI keyMoveLeftText;
    [SerializeField] private TextMeshProUGUI keyMoveRightText;

    // ---Keyboard actions---
    [SerializeField] private TextMeshProUGUI keyInteractText;
    [SerializeField] private TextMeshProUGUI keyAltInteractText;
    [SerializeField] private TextMeshProUGUI keyPauseText;


    // ---GamePad actions---
    [SerializeField] private TextMeshProUGUI keyGamepadInteractText;
    [SerializeField] private TextMeshProUGUI keyGamepadAltInteractText;
    [SerializeField] private TextMeshProUGUI keyGamepadPauseText;

    private void Start()
    {
        GameInput.Instance.OnKeyRebind += GameInput_OnKeyRebind;
        GameStateManager.Instace.OnStateChanged += GameStateManager_OnStateChanged;

        UpdateVisual();

        Show();
    }

    private void GameStateManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameStateManager.Instace.IsCountdownToStartActive())
        {
            Hide();
        }
    }

    private void GameInput_OnKeyRebind(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        // ---Update keybind button text---
        keyMoveUpText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.Move_Up);
        keyMoveDownText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.Move_Down);
        keyMoveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.Move_Left);
        keyMoveRightText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.Move_Right);
        keyInteractText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.Interact);
        keyAltInteractText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.InteractAlternate);
        keyPauseText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.Pause);
        keyGamepadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.Gamepad_Interact);
        keyGamepadAltInteractText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.Gamepad_InteractAlternate);
        keyGamepadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.Gamepad_Pause);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }


}
