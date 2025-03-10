using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class MultiCanvasAssigner : MonoBehaviour
{
    [SerializeField] private Transform[] panels;

    private PlayerInputManager inputManager;
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

        input.uiInputModule = inputModule;
        panels[input.playerIndex].gameObject.SetActive(true);
        activePanelCount++;

        panels[input.playerIndex].GetComponent<MultiButtonListener>().AddButtonListener(input, activePanelCount);
    }
}
