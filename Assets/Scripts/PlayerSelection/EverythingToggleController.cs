using System;
using UnityEngine;
using UnityEngine.UI;

public class EverythingToggleController : MonoBehaviour
{
    [SerializeField] private Toggle obstacleToggle, powerupToggle;
    [SerializeField] private Toggle[] obstacles, powerups;
    [SerializeField] private GameManagerSO manager;

    void OnEnable()
    {
        foreach (Toggle obstacle in obstacles)
        {
            obstacle.onValueChanged.RemoveListener(UpdateObstacleToggle);
            obstacle.onValueChanged.AddListener(UpdateObstacleToggle);
        }

        foreach (Toggle powerup in powerups)
        {
            powerup.onValueChanged.RemoveListener(UpdatePowerupToggle);
            powerup.onValueChanged.AddListener(UpdatePowerupToggle);
        }
    }

    void OnDisable()
    {
        foreach (Toggle obstacle in obstacles)
        {
            obstacle.onValueChanged.RemoveListener(UpdateObstacleToggle);
        }

        foreach (Toggle powerup in powerups)
        {
            powerup.onValueChanged.RemoveListener(UpdatePowerupToggle);
        }
    }

    private void UpdateObstacleToggle(bool isOn)
    {
        bool newState = manager.EveryObstacle();

        if (obstacleToggle.isOn != newState)
        {
            obstacleToggle.onValueChanged.RemoveListener(UpdateObstacleToggle);
            obstacleToggle.SetIsOnWithoutNotify(newState);
            obstacleToggle.onValueChanged.AddListener(UpdateObstacleToggle);
        }
    }

    private void UpdatePowerupToggle(bool isOn)
    {
        bool newState = manager.EveryPowerUp();

        if (powerupToggle.isOn != newState)
        {
            powerupToggle.onValueChanged.RemoveListener(UpdatePowerupToggle);
            powerupToggle.SetIsOnWithoutNotify(newState);
            powerupToggle.onValueChanged.AddListener(UpdatePowerupToggle);
        }
    }
}
