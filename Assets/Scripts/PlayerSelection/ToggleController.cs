using System;
using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    [SerializeField] private Toggle everyObstacleToggle, everyPowerupToggle, nothingObstacleToggle, nothingPowerupToggle;
    [SerializeField] private Toggle[] obstacles, powerups;
    [SerializeField] private GameManagerSO manager;

    public void EveryObstacle(bool isOn)
    {
        bool newState = manager.EveryObstacle();

        if (everyObstacleToggle.isOn != newState)
        {
            everyObstacleToggle.SetIsOnWithoutNotify(newState);
        }
    }

    public void EveryPowerup(bool isOn)
    {
        bool newState = manager.EveryPowerUp();

        if (everyPowerupToggle.isOn != newState)
        {
            everyPowerupToggle.SetIsOnWithoutNotify(newState);
        }
    }

    public void NothingObstacle(bool isOn)
    {
        nothingObstacleToggle.isOn = manager.NoObstacle();
    }

    public void NothingPowerup(bool isOn)
    {
        nothingPowerupToggle.isOn = manager.NoPowerUp();
    }

    public void AntiObstacle(bool isOn)
    {
        if (!isOn) return;

        everyObstacleToggle.isOn = false;
    }

    public void AntiPowerup(bool isOn)
    {
        if (!isOn) return;

        everyPowerupToggle.isOn = false;
    }
}
