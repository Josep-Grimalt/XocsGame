using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class MultiCanvasAssigner : MonoBehaviour
{
    [SerializeField] private Transform[] panels;
    [SerializeField] private Button startButton;

    private PlayerInputManager inputManager;
    private Button[] buttons;
    private int ready = 0;
    private int activePanelCount = 0;

    void Awake()
    {
        inputManager = GetComponent<PlayerInputManager>();
    }

    void OnEnable()
    {
        inputManager.onPlayerJoined += ClaimPanel;
    }

    private void ClaimPanel(PlayerInput input)
    {
        InputSystemUIInputModule inputModule = panels[input.playerIndex].GetComponentInChildren<InputSystemUIInputModule>();

        input.gameObject.GetComponent<PlayerInput>().uiInputModule = inputModule;
        panels[input.playerIndex].gameObject.SetActive(true);
        activePanelCount++;

        buttons = panels[input.playerIndex].GetComponentsInChildren<Button>();
        AddButtonListener(input);
    }

    private void AddButtonListener(PlayerInput input)
    {
        buttons[0].onClick.AddListener(() => PrevColor(input));
        buttons[1].onClick.AddListener(() => NextColor(input));
        buttons[2].onClick.AddListener(() => PrevModel(input));
        buttons[3].onClick.AddListener(() => NextModel(input));
        buttons[4].onClick.AddListener(() =>
        {
            Ready(input);
            
            if (ready == activePanelCount)
            {
                startButton.interactable = true;
            }
            else
            {
                startButton.interactable = false;
            }
        });
        Debug.Log("Active Panel Count: " + activePanelCount);
    }

    private void Ready(PlayerInput input)
    {
        ready = input.gameObject.GetComponent<UIController>().Ready(true, ready);
        Debug.Log("Players Ready: " + ready);
    }

    private void PrevModel(PlayerInput input)
    {
        input.gameObject.GetComponent<UIController>().SelectModel(false);
    }

    private void NextModel(PlayerInput input)
    {
        input.gameObject.GetComponent<UIController>().SelectModel(true);
    }

    private void PrevColor(PlayerInput input)
    {
        input.gameObject.GetComponent<UIController>().SelectColor(false);
    }

    private void NextColor(PlayerInput input)
    {
        input.gameObject.GetComponent<UIController>().SelectColor(true);
    }
}
