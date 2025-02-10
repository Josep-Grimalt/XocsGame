using System.Collections;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private float delay;

    private void OnEnable() {
        // if(!GetComponent<GameManager>().Obstacles()) return;

        StartCoroutine(SpawnObstacle());
    }

    private IEnumerator SpawnObstacle()
    {
        while(true)
        {
            yield return new WaitForSeconds(delay);

            Instantiate(obstacles[Random.Range(0, obstacles.Length)], new (Random.Range(-9,9), 5, Random.Range(-9,9)), Quaternion.identity);
        }
    }
}
