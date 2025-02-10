using System.Collections;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private float delay;
    [SerializeField] private int minScale, maxScale;

    private void OnEnable()
    {
        // if(!GetComponent<GameManager>().Obstacles()) return;

        StartCoroutine(SpawnObstacle());
    }

    private IEnumerator SpawnObstacle()
    {
        GameObject go;

        while (true)
        {
            yield return new WaitForSeconds(delay);

            go = Instantiate(obstacles[Random.Range(0, obstacles.Length)], new(Random.Range(-9f, 9f), 5, Random.Range(-9f, 9f)), Quaternion.identity);
            go.transform.Rotate(new(0, Random.Range(0, 360), 0));
            if (Random.Range(0, 2) % 2 == 0)
            {
                go.transform.localScale = new(Random.Range(minScale, maxScale), 5, 1);
            }
            else
            {
                go.transform.localScale = new(1, 5, Random.Range(minScale, maxScale));
            }
        }
    }
}
