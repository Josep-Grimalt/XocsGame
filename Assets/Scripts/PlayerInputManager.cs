using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerInputManager : MonoBehaviour
{
    private PlayerMovementController movementController;
    private PlayerAction inputActions;

    private void Awake() {
        movementController = GetComponent<PlayerMovementController>();
    }


    void OnEnable()
    {
        inputActions = new PlayerAction();
        inputActions.Player.Enable();

        inputActions.Player.Move.performed += Move;
        inputActions.Player.Move.canceled += Move;
    }


    void OnDisable()
    {
        inputActions.Player.Move.performed -= Move;
        inputActions.Player.Move.canceled -= Move;

        inputActions.Player.Disable();        
    }

    private void Move(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();

        movementController.SetMovementDirection(inputVector);
    }
}
