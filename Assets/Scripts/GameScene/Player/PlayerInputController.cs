using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private PlayerInput inputActions;
    [SerializeField] private List<GameObject> colors;
    
    private PlayerMovementController movementController;

    private InputAction moveAction;
    private InputAction selectColorAction;
    private int color = 1;

    private void Awake() {
        movementController = GetComponent<PlayerMovementController>();

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
        if (context.ReadValue<float>() == 1)
        {
            if (color == 12)
            {
                color = 1;
            }
            else
            {
                color++;
            }
        }
        else if (context.ReadValue<float>() == -1)
        {
            if (color == 1)
            {
                color = 12;
            }
            else
            {
                color--;
            }
        }

        switch (color)
        {
            case 1:
                foreach (GameObject colorOject in colors)
                {
                    if (colorOject.name == "Color 1")
                    {
                        colorOject.SetActive(true);
                    }
                    else
                    {
                        colorOject.SetActive(false);
                    }
                }
                break;

            case 2:
                foreach (GameObject colorOject in colors)
                {
                    if (colorOject.name == "Color 2")
                    {
                        colorOject.SetActive(true);
                    }
                    else
                    {
                        colorOject.SetActive(false);
                    }
                }
                break;

            case 3:
                foreach (GameObject colorOject in colors)
                {
                    if (colorOject.name == "Color 3")
                    {
                        colorOject.SetActive(true);
                    }
                    else
                    {
                        colorOject.SetActive(false);
                    }
                }
                break;

            case 4:
                foreach (GameObject colorOject in colors)
                {
                    if (colorOject.name == "Color 4")
                    {
                        colorOject.SetActive(true);
                    }
                    else
                    {
                        colorOject.SetActive(false);
                    }
                }
                break;

            case 5:
                foreach (GameObject colorOject in colors)
                {
                    if (colorOject.name == "Color 5")
                    {
                        colorOject.SetActive(true);
                    }
                    else
                    {
                        colorOject.SetActive(false);
                    }
                }
                break;

            case 6:
                foreach (GameObject colorOject in colors)
                {
                    if (colorOject.name == "Color 6")
                    {
                        colorOject.SetActive(true);
                    }
                    else
                    {
                        colorOject.SetActive(false);
                    }
                }
                break;
            case 7:
                foreach (GameObject colorOject in colors)
                {
                    if (colorOject.name == "Color 7")
                    {
                        colorOject.SetActive(true);
                    }
                    else
                    {
                        colorOject.SetActive(false);
                    }
                }
                break;

            case 8:
                foreach (GameObject colorOject in colors)
                {
                    if (colorOject.name == "Color 8")
                    {
                        colorOject.SetActive(true);
                    }
                    else
                    {
                        colorOject.SetActive(false);
                    }
                }
                break;

            case 9:
                foreach (GameObject colorOject in colors)
                {
                    if (colorOject.name == "Color 9")
                    {
                        colorOject.SetActive(true);
                    }
                    else
                    {
                        colorOject.SetActive(false);
                    }
                }
                break;

            case 10:
                foreach (GameObject colorOject in colors)
                {
                    if (colorOject.name == "Color 10")
                    {
                        colorOject.SetActive(true);
                    }
                    else
                    {
                        colorOject.SetActive(false);
                    }
                }
                break;

            case 11:
                foreach (GameObject colorOject in colors)
                {
                    if (colorOject.name == "Color 11")
                    {
                        colorOject.SetActive(true);
                    }
                    else
                    {
                        colorOject.SetActive(false);
                    }
                }
                break;

            case 12:
                foreach (GameObject colorOject in colors)
                {
                    if (colorOject.name == "Color 12")
                    {
                        colorOject.SetActive(true);
                    }
                    else
                    {
                        colorOject.SetActive(false);
                    }
                }
                break;
        }
        
        Debug.Log(color);
    }
}
