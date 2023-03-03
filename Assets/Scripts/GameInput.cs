using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{

    private const string PLAYER_PREFS_BINDINGS = "InputBindings";

    public static GameInput Instance { get; private set; }

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlteranteAction;
    public event EventHandler OnPauseAction;
    public event EventHandler OnKeyRebind;

    public enum Bindings
    {
        Move_Up,
        Move_Down,
        Move_Left,
        Move_Right,
        Interact,
        InteractAlternate,
        Pause,
        Gamepad_Interact,
        Gamepad_InteractAlternate,
        Gamepad_Pause,
    }

    private PlayerInputActions playerInputActions;


    private void Awake()
    {
        Instance = this;

        playerInputActions = new PlayerInputActions();



        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
        {
            playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        }

        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerInputActions.Player.Pause.performed += Pause_performed;
    }

    private void OnDestroy()
    {
        playerInputActions.Player.Interact.performed -= Interact_performed;
        playerInputActions.Player.InteractAlternate.performed -= InteractAlternate_performed;
        playerInputActions.Player.Pause.performed -= Pause_performed;

        playerInputActions.Dispose();
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlteranteAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }

    public string GetBindingText(Bindings bindings)
    {
        switch (bindings)
        {
            default:
            case Bindings.Move_Up:
                return playerInputActions.Player.Move.bindings[1].ToDisplayString();
            case Bindings.Move_Down:
                return playerInputActions.Player.Move.bindings[2].ToDisplayString();
            case Bindings.Move_Left:
                return playerInputActions.Player.Move.bindings[3].ToDisplayString();
            case Bindings.Move_Right:
                return playerInputActions.Player.Move.bindings[4].ToDisplayString();
            case Bindings.Interact:
                return playerInputActions.Player.Interact.bindings[0].ToDisplayString();
            case Bindings.InteractAlternate:
                return playerInputActions.Player.InteractAlternate.bindings[0].ToDisplayString();
            case Bindings.Pause:
                return playerInputActions.Player.Pause.bindings[0].ToDisplayString();
            case Bindings.Gamepad_Interact:
                return playerInputActions.Player.Interact.bindings[1].ToDisplayString();
            case Bindings.Gamepad_InteractAlternate:
                return playerInputActions.Player.InteractAlternate.bindings[1].ToDisplayString();
            case Bindings.Gamepad_Pause:
                return playerInputActions.Player.Pause.bindings[1].ToDisplayString();
        }
    }

    public void RebindButton(Bindings bind, Action onActionRebound)
    {
        playerInputActions.Player.Disable();

        InputAction inputAction;
        int bindIndex;

        switch (bind)
        {
            default:
            case Bindings.Move_Up:
                inputAction = playerInputActions.Player.Move;
                bindIndex = 1;
                break;
            case Bindings.Move_Down:
                inputAction = playerInputActions.Player.Move;
                bindIndex = 2;
                break;
            case Bindings.Move_Left:
                inputAction = playerInputActions.Player.Move;
                bindIndex = 3;
                break;
            case Bindings.Move_Right:
                inputAction = playerInputActions.Player.Move;
                bindIndex = 4;
                break;
            case Bindings.Interact:
                inputAction = playerInputActions.Player.Interact;
                bindIndex = 0;
                break;
            case Bindings.InteractAlternate:
                inputAction = playerInputActions.Player.InteractAlternate;
                bindIndex = 0;
                break;
            case Bindings.Pause:
                inputAction = playerInputActions.Player.Pause;
                bindIndex = 0;
                break;
            case Bindings.Gamepad_Interact:
                inputAction = playerInputActions.Player.Interact;
                bindIndex = 1;
                break;
            case Bindings.Gamepad_InteractAlternate:
                inputAction = playerInputActions.Player.InteractAlternate;
                bindIndex = 1;
                break;
            case Bindings.Gamepad_Pause:
                inputAction = playerInputActions.Player.Pause;
                bindIndex = 1;
                break;
        }

        inputAction.PerformInteractiveRebinding(bindIndex).OnComplete(callback => {
            playerInputActions.Player.Enable();
            onActionRebound();

            string playerOveridesJson = playerInputActions.SaveBindingOverridesAsJson();
            PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, playerOveridesJson);
            PlayerPrefs.Save();

            OnKeyRebind?.Invoke(this, EventArgs.Empty);
        }).Start();
    }

}
