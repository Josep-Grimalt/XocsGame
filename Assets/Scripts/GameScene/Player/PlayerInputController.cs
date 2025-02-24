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
    private InputAction selectorVerticalAction;
    private InputAction selectorHorizontalAction;

    private void Awake() {
        movementController = GetComponent<PlayerMovementController>();
        uiController = GetComponent<UIController>();

        selectColorAction = inputActions.actions.FindAction("SelectColor");
        selectorVerticalAction = inputActions.actions.FindAction("SelectorVertical");
        selectorHorizontalAction = inputActions.actions.FindAction("SelectorHorizontal");
        moveAction = inputActions.actions.FindAction("Move");
    }


    void OnEnable()
    {
        selectColorAction.performed += SelectColor;
        selectorVerticalAction.performed += SelectorVertical;
        selectorHorizontalAction.performed += SelectorHorizontal;
        
        if (SceneManager.GetActiveScene().buildIndex > 1)
        {
            moveAction.performed += Move;
            moveAction.canceled += Move;
        }
    }


    void OnDisable()
    {
        selectColorAction.performed -= SelectColor;
        selectorVerticalAction.performed -= SelectorVertical;
        selectorHorizontalAction.performed -= SelectorHorizontal;
        
        if (SceneManager.GetActiveScene().buildIndex > 1)
        {
            moveAction.performed -= Move;
            moveAction.canceled -= Move;
        }
    }

    private void SelectColor(InputAction.CallbackContext context)
    {
        uiController.SelectColor(context.ReadValue<bool>());
    }

    private void SelectorVertical(InputAction.CallbackContext context)
    {
        
    }

    private void SelectorHorizontal(InputAction.CallbackContext context)
    {
        
    }

    private void Move(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();

        movementController.SetMovementDirection(inputVector);
    }
}
