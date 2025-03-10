using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private PlayerInput inputActions;
    
    private PlayerMovementController movementController;
    private InputAction moveAction;

    private void Awake() {
        movementController = GetComponent<PlayerMovementController>();

        moveAction = inputActions.actions.FindAction("Move");
    }


    void OnEnable()
    {
        moveAction.performed += Move;
        moveAction.canceled += Move;
    }

    void OnDisable()
    {
        moveAction.performed -= Move;
        moveAction.canceled -= Move;
    }

    private void Move(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();

        movementController.SetMovementDirection(inputVector);
    }
}
