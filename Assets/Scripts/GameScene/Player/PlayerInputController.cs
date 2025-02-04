using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private PlayerInput inputActions;
    
    private PlayerMovementController movementController;
    private UIController uiController;

    private InputAction moveAction;
    private InputAction selectColorAction;

    private void Awake() {
        movementController = GetComponent<PlayerMovementController>();
        uiController = GetComponent<UIController>();

        moveAction = inputActions.actions.FindAction("Move");
        selectColorAction = inputActions.actions.FindAction("SelectColor");
    }


    void OnEnable()
    {
        moveAction.performed += Move;
        moveAction.canceled += Move;

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            selectColorAction.performed += SelectColor;
        }
    }


    void OnDisable()
    {
        moveAction.performed -= Move;
        moveAction.canceled -= Move;

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            selectColorAction.performed -= SelectColor;
        }

    }

    private void Move(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();

        movementController.SetMovementDirection(inputVector);
    }

    private void SelectColor(InputAction.CallbackContext context)
    {
        uiController.SelectColor(context);
    }
}
