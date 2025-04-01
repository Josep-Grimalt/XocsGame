using UnityEngine;

public class GameManagerSOReset : MonoBehaviour
{
    [SerializeField] private GameManagerSO gameManager;

    void Awake()
    {
        gameManager.SetFallingPlatform(true);
        gameManager.SetWalls(true);
        gameManager.SetElements(true);
        gameManager.SetAllObstacles(false);
        gameManager.SetAllPowerUps(false);
    }
}
