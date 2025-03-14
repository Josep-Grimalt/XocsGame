using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "GameManager", menuName = "SCO/GameManager")]
public class GameManagerSO : ScriptableObject
{
    [SerializeField] private bool fallingPlatform;
    [SerializeField] private bool walls, elements;
    [SerializeField] private bool[] powerups, obstacles;

    private static GameManagerSO instance;
    private int number;

    public static GameManagerSO Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<GameManagerSO>("GameManager");
            }
            return instance;
        }
    }

    public bool[] GetPowerups()
    {
        return powerups;
    }

    public bool[] GetObstacles()
    {
        return obstacles;
    }

    public bool Walls()
    {
        return walls;
    }

    public bool FallingPlatforms()
    {
        return fallingPlatform;
    }

    public bool Elements()
    {
        return elements;
    }

    public void SetFallingPlatform(bool f)
    {
        fallingPlatform = f;
    }

    public void SetWalls(bool f)
    {
        walls = f;
    }

    public void SetElements(bool f)
    {
        elements = f;
    }

    public void PowerUps(bool f)
    {
        powerups[number] = f;
    }

    public void Obstacles(bool f)
    {
        obstacles[number] = f;
    }

    public void SetNumber(int n)
    {
        number = n;
    }

    public bool EveryObstacle()
    {
        return obstacles.Contains(false);
    }

    public bool EveryPowerUp()
    {
        return powerups.Contains(false);
    }
}
