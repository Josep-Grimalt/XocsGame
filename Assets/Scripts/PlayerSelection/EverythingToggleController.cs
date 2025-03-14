using System;
using UnityEngine;
using UnityEngine.UI;

public class EverythingToggleController : MonoBehaviour
{
    [SerializeField] private Toggle everyObstacleToggle, everyPowerupToggle, nothingObstacleToggle, nothingPowerupToggle;
    [SerializeField] private Toggle[] obstacles, powerups;
    [SerializeField] private GameManagerSO manager;

    public void UpdateObstacleToggle(bool isOn)
    {
        bool newState = manager.EveryObstacle();

        if (everyObstacleToggle.isOn != newState)
        {
            everyObstacleToggle.onValueChanged.RemoveListener(UpdateObstacleToggle);
            everyObstacleToggle.SetIsOnWithoutNotify(newState);
            everyObstacleToggle.onValueChanged.AddListener(UpdateObstacleToggle);
        }
    }

    public void UpdatePowerupToggle(bool isOn)
    {
        bool newState = manager.EveryPowerUp();

        if (everyPowerupToggle.isOn != newState)
        {
            everyPowerupToggle.onValueChanged.RemoveListener(UpdatePowerupToggle);
            everyPowerupToggle.SetIsOnWithoutNotify(newState);
            everyPowerupToggle.onValueChanged.AddListener(UpdatePowerupToggle);
        }
    }

    public void NothingObstacle(bool isOn)
    {
        nothingObstacleToggle.isOn = manager.NoObstacle();
    }

    public void NothingPowerup(bool isOn)
    {
        nothingPowerupToggle .isOn = manager.NoPowerUp();
    }
}
