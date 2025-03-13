using UnityEngine;
using UnityEngine.UI;

public class CustomToggle : Toggle
{
    [SerializeField] private int number;
    [SerializeField] private bool powerUp, obstacle;
    [SerializeField] private GameManagerSO gameManagerSo;
    
    private bool[] powerUps, obstacles;

    protected override void Awake()
    {
        base.Awake();
        powerUps = gameManagerSo.GetPowerups();
        obstacles = gameManagerSo.GetObstacles();
    }

    protected override void Start()
    {
        base.Start();
        onValueChanged.AddListener(HandleValueChanged);
    }

    private void HandleValueChanged(bool value)
    {
        if (powerUp)
        {
            obstacle = false;
            
            for (int i = 0; i < powerUps.Length; i++)
            {
                if (i == number)
                    powerUps[i] = value;
            }
            
            gameManagerSo.SetPowerUps(powerUps);
        }

        if (obstacle)
        {
            powerUp = false;
            
            for (int i = 0; i < obstacles.Length; i++)
            {
                if (i == number)
                    obstacles[i] = value;
            }
            
            gameManagerSo.SetObstacles(obstacles);
        }
    }
}
