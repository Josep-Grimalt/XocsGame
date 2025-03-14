using System;
using UnityEngine;
using UnityEngine.UI;

public class EverythingToggleController : MonoBehaviour
{
    [SerializeField] private Toggle obstacleToggle, powerupToggle;
    [SerializeField] private Toggle[] obstacles, powerups;

    void OnEnable()
    {
        foreach (Toggle obstacle in obstacles)
        {
            obstacle.onValueChanged.AddListener(UpdateObstacleToggle);
        }

        foreach (Toggle powerup in powerups)
        {
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

    private void UpdateObstacleToggle(bool arg0)
    {
        obstacleToggle.isOn = GameManagerSO.Instance.EveryObstacle();
    }

    private void UpdatePowerupToggle(bool arg0)
    {
        powerupToggle.isOn = GameManagerSO.Instance.EveryPowerUp();
    }
}
