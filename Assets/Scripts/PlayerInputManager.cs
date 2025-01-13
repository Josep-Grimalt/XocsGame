using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerInputManager : MonoBehaviour
{
    private PlayerMovementController movementController;

    private InputAction moveAction;
    [SerializeField] private PlayerInput inputActions;

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
