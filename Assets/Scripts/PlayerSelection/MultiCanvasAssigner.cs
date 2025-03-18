using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class MultiCanvasAssigner : MonoBehaviour
{
    [SerializeField] private Transform[] panels;
    [SerializeField] private Button startButton;
    [SerializeField] private GameObject join;

    private Button[][] buttons;
    private PlayerInputManager inputManager;
    private static int _activePanelCount = 0;
    private static int _readyCount = 0;

    void Awake()
    {
        inputManager = GetComponent<PlayerInputManager>();
        buttons = new Button[panels.Length][];
        _activePanelCount = 0;
    }

    void OnEnable()
    {
        inputManager.onPlayerJoined += ClaimPanel;
        inputManager.onPlayerLeft += RemovePanel;
    }

    void OnDisable()
    {
        inputManager.onPlayerJoined -= ClaimPanel;
        inputManager.onPlayerLeft -= RemovePanel;
    }

    private void ClaimPanel(PlayerInput input)
    {
        InputSystemUIInputModule inputModule = panels[input.playerIndex].GetComponentInChildren<InputSystemUIInputModule>();

        input.uiInputModule = inputModule;
        panels[input.playerIndex].gameObject.SetActive(true);
        _activePanelCount++;
        startButton.interactable = false;

        buttons[input.playerIndex] = panels[input.playerIndex].gameObject.GetComponentsInChildren<Button>().ToArray();
        AddButtonListener(input);
        join.SetActive(false);
    }

    private void RemovePanel(PlayerInput input)
    {
        panels[input.playerIndex].gameObject.SetActive(false);
        _activePanelCount--;
        buttons[input.playerIndex][0].onClick.RemoveAllListeners();
        buttons[input.playerIndex][1].onClick.RemoveAllListeners();
        buttons[input.playerIndex][2].onClick.RemoveAllListeners();
        buttons[input.playerIndex][3].onClick.RemoveAllListeners();
        buttons[input.playerIndex][4].onClick.RemoveAllListeners();

        if (inputManager.playerCount < 1)
        {
            join.SetActive(true);
        }
    }

    public void AddButtonListener(PlayerInput input)
    {
        buttons[input.playerIndex][0].onClick.AddListener(() => PrevColor(input));
        buttons[input.playerIndex][1].onClick.AddListener(() => NextColor(input));
        buttons[input.playerIndex][2].onClick.AddListener(() => PrevModel(input));
        buttons[input.playerIndex][3].onClick.AddListener(() => NextModel(input));
        buttons[input.playerIndex][4].onClick.AddListener(() =>
        {
            Ready(input);

            if (input.gameObject.GetComponent<UIController>().GetReady()[input.playerIndex])
            {
                for (int i = 0; i < buttons[input.playerIndex].Length; i++)
                {
                    if (i < 4)
                    {
                        buttons[input.playerIndex][i].interactable = false;
                    }
                }
                SoundManager.I.PlaySound(SoundName.Ready);
            }
            else
            {
                for (int i = 0; i < buttons[input.playerIndex].Length; i++)
                {
                    if (i < 4)
                    {
                        buttons[input.playerIndex][i].interactable = true;
                    }
                }
            }

            if (_readyCount == _activePanelCount)
            {
                startButton.interactable = true;
            }
            else
            {
                startButton.interactable = false;
            }
        });
    }

    private void Ready(PlayerInput input)
    {
        _readyCount = input.gameObject.GetComponent<UIController>().Ready(input.playerIndex, true);
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
