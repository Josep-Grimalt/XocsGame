using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MultiButtonListener : MonoBehaviour
{
    [SerializeField] private Button prevColor, nextColor, prevModel, nextModel, ready;
    [SerializeField] private TextMeshProUGUI model;
    [SerializeField] private Button startButton;

    private PlayerInput player;

    public void setPlayerInput(PlayerInput input)
    {
        player = input;
    }
    

    private static int _readyCount = 0;

    public void AddButtonListener(PlayerInput input, int activePanelCount)
    {
        prevColor.onClick.AddListener(() => PrevColor(input));
        nextColor.onClick.AddListener(() => NextColor(input));
        prevModel.onClick.AddListener(() => PrevModel(input));
        nextModel.onClick.AddListener(() => NextModel(input));
        ready.onClick.AddListener(() =>
        {
            Ready(input);
            
            if (_readyCount == activePanelCount)
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
        _readyCount = input.gameObject.GetComponent<UIController>().Ready(true, _readyCount);
        Debug.Log(_readyCount);
    }

    private void PrevModel(PlayerInput input)
    {
        model.SetText(input.gameObject.GetComponent<UIController>().SelectModel(false));
    }

    private void NextModel(PlayerInput input)
    {
        model.SetText(input.gameObject.GetComponent<UIController>().SelectModel(true));
    }

    private void PrevColor(PlayerInput input)
    {
        input.gameObject.GetComponent<UIController>().SelectColor(false, GetComponent<PanelGetColorAndGetModel>().GetColors());
    }

    private void NextColor(PlayerInput input)
    {
        input.gameObject.GetComponent<UIController>().SelectColor(true, GetComponent<PanelGetColorAndGetModel>().GetColors());
    }
}
