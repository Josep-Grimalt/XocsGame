using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private PlayerInput inputActions;

    private PlayerMovementController movementController;
    private InputAction moveAction, cancelAction;

    private void Awake()
    {
        movementController = GetComponent<PlayerMovementController>();

        moveAction = inputActions.actions.FindAction("Move");
        cancelAction = inputActions.actions.FindAction("Cancel");
    }


    void OnEnable()
    {
        moveAction.performed += Move;
        moveAction.canceled += Move;
        cancelAction.performed += Disconnect;
    }

    void OnDisable()
    {
        moveAction.performed -= Move;
        moveAction.canceled -= Move;
        cancelAction.performed -= Disconnect;
    }

    private void Move(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();

        movementController.SetMovementDirection(inputVector);
    }

    private void Disconnect(InputAction.CallbackContext context)
    {
        if (SceneManager.GetActiveScene().buildIndex != 1) return;

        inputActions.DeactivateInput();
        Destroy(gameObject);
    }
}
