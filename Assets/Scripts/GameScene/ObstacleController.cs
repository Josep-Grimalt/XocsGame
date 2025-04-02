using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float delay;
    [SerializeField] private float minScale = 2.5f, maxScale = 10;
    [SerializeField] private int maxObstacles = 5;

    private void OnEnable()
    {
        if (!gameManager.Obstacles().Contains(true)) return;

        StartCoroutine(SpawnObstacle());
    }

    private IEnumerator SpawnObstacle()
    {
        GameObject go;
        int i;

        while (gameObject.activeSelf)
        {
            if (!gameManager.IsGameReady())
            {
                yield return null;
                continue;
            }

            yield return new WaitForSeconds(delay);

            do
            {
                i = Random.Range(0, gameManager.Obstacles().Length);
            } while (!gameManager.Obstacles()[i]);

            go = Instantiate(obstacles[i], new(Random.Range(-9f, 9f), 0f, Random.Range(-9f, 9f)), Quaternion.identity);
            go.transform.Rotate(new(0, Random.Range(0, 360), 0));
            if (Random.Range(0, 2) % 2 == 0)
            {
                go.transform.localScale = new(Random.Range(minScale, maxScale), 5, 1);
            }
            else
            {
                go.transform.localScale = new(1, 5, Random.Range(minScale, maxScale));
            }

            if (go.TryGetComponent(out NavMeshObstacle obstacle))
            {
                obstacle.enabled = true;
            }

            Destroy(go, delay * maxObstacles);
        }
    }
}