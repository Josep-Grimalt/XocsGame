using UnityEngine;

[CreateAssetMenu(fileName = "GameManager", menuName = "Scriptable Objects/GameManager")]
public class GameManagerSO : ScriptableObject
{
    [SerializeField] private bool fallingPlatform;
    [SerializeField] private bool walls, elements;
    [SerializeField] private bool[] powerups, obstacles;

    private static GameManagerSO instance;

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

    public bool[] Powerups()
    {
        return powerups;
    }

    public bool[] Obstacles()
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

    public void SetPowerUps(int f)
    {
        switch (f)
        {
            case 0:
                for (int i = 0; i < powerups.Length; i++)
                {
                    powerups[i] = false;
                }
                break;
            case 1 - 2:
                for (int i = 0; i < powerups.Length; i++)
                {
                    if (i + 1 != f)
                        powerups[i] = false;
                    else
                    {
                        powerups[i] = true;
                    }
                }
                break;
            default:
                for (int i = 0; i < powerups.Length; i++)
                {
                    powerups[i] = true;
                }
                break;
        }
    }

    public void SetObstacles(int f)
    {
        switch (f)
        {
            case 0:
                for (int i = 0; i < obstacles.Length; i++)
                {
                    obstacles[i] = false;
                }
                break;
            default:
                for (int i = 0; i < obstacles.Length; i++)
                {
                    obstacles[i] = true;
                }
                break;
        }
    }
}
