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

    private PlayerInputManager inputManager;
    private Button[][] buttons;
    private GameObject[][] colorsUI;
    private TextMeshProUGUI[] model;
    private int ready = 0;
    private int activePanelCount = 0;

    void Awake()
    {
        inputManager = GetComponent<PlayerInputManager>();
        buttons = new Button[panels.Length][];
        colorsUI = new GameObject[panels.Length][];
        model = new TextMeshProUGUI[panels.Length];
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

        buttons[input.playerIndex] = panels[input.playerIndex].GetComponentsInChildren<Button>();
        colorsUI[input.playerIndex] = panels[input.playerIndex].GetComponent<PanelGetColorAndGetModel>().GetColors().ToArray();
        model[input.playerIndex] = panels[input.playerIndex].GetComponent<PanelGetColorAndGetModel>().GetModel();
        AddButtonListener(input);
    }

    private void AddButtonListener(PlayerInput input)
    {
        buttons[input.playerIndex][0].onClick.AddListener(() => PrevColor(input));
        buttons[input.playerIndex][1].onClick.AddListener(() => NextColor(input));
        buttons[input.playerIndex][2].onClick.AddListener(() => PrevModel(input));
        buttons[input.playerIndex][3].onClick.AddListener(() => NextModel(input));
        buttons[input.playerIndex][4].onClick.AddListener(() =>
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
    }

    private void Ready(PlayerInput input)
    {
        ready = input.gameObject.GetComponent<UIController>().Ready(true, ready);
    }

    private void PrevModel(PlayerInput input)
    {
        model[input.playerIndex].SetText(input.gameObject.GetComponent<UIController>().SelectModel(false));
    }

    private void NextModel(PlayerInput input)
    {
        model[input.playerIndex].SetText(input.gameObject.GetComponent<UIController>().SelectModel(true));
    }

    private void PrevColor(PlayerInput input)
    {
        input.gameObject.GetComponent<UIController>().SelectColor(false, colorsUI[input.playerIndex].ToList());
    }

    private void NextColor(PlayerInput input)
    {
        input.gameObject.GetComponent<UIController>().SelectColor(true, colorsUI[input.playerIndex].ToList());
    }
}
