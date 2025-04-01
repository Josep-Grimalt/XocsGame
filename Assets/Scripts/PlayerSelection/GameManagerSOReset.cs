using UnityEngine;

public class GameManagerSOReset : MonoBehaviour
{
    [SerializeField] private GameManagerSO gameManager;

    void Awake()
    {
        gameManager.SetFallingPlatform(false);
        gameManager.SetWalls(true);
        gameManager.SetElements(true);
        gameManager.SetAllObstacles(false);
        gameManager.SetAllPowerUps(false);
    }
}
