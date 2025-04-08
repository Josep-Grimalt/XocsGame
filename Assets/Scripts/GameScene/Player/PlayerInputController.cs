using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private PlayerInput inputActions;

    private PlayerMovementController movementController;
    private UIController isReady;
    private InputAction moveAction, cancelAction, clickAction, navigateAction;

    private void Awake()
    {
        movementController = GetComponent<PlayerMovementController>();
        isReady = GetComponent<UIController>();

        moveAction = inputActions.actions.FindAction("Move");
        cancelAction = inputActions.actions.FindAction("Cancel");
        clickAction = inputActions.actions.FindAction("Click");
        navigateAction = inputActions.actions.FindAction("Navigate");
    }


    void OnEnable()
    {
        moveAction.performed += Move;
        moveAction.canceled += Move;
        cancelAction.performed += Disconnect;
        clickAction.performed += Click;
        navigateAction.performed += MenuNavigation;
    }

    void OnDisable()
    {
        moveAction.performed -= Move;
        moveAction.canceled -= Move;
        cancelAction.performed -= Disconnect;
        clickAction.performed -= Click;
        navigateAction.performed -= MenuNavigation;
    }

    private void Move(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();

        movementController.SetMovementDirection(inputVector);
    }

    private void Disconnect(InputAction.CallbackContext context)
    {
        if (SceneManager.GetActiveScene().buildIndex != 2 || isReady.GetReady()[inputActions.playerIndex]) return;

        inputActions.DeactivateInput();
        Destroy(gameObject);
    }

    private void Click(InputAction.CallbackContext context)
    {
        if (SceneManager.GetActiveScene().buildIndex > 2) return;

        SoundManager.I.PlaySound(SoundName.MenuClick);
    }

    private void MenuNavigation(InputAction.CallbackContext context)
    {
        if (SceneManager.GetActiveScene().buildIndex > 2) return;

        SoundManager.I.PlaySound(SoundName.MenuNavigation);
    }
}
