using System.Collections;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    [SerializeField] private GameObject[] powerups;
    [SerializeField] private float delay = 5;

    private void OnEnable()
    {
        if (GameObject.Find("Game Manager").GetComponent<GameManager>().Powerups())
            StartCoroutine(SpawnPowerup());
    }

    private IEnumerator SpawnPowerup()
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);

            PoolManager.SpawnObject(powerups[Random.Range(0, powerups.Length)], new(Random.Range(-9f, 9), 5, Random.Range(-9, 9f)), Quaternion.identity);
        }
    }
}
