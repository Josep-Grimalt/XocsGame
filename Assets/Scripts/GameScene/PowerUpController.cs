using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    [SerializeField] private GameObject[] powerups;
    [SerializeField] private float delay = 5;
    [SerializeField] private GameManager gameManager;

    private void OnEnable()
    {
        if (!gameManager.Powerups().Contains(true)) return;

        StartCoroutine(SpawnPowerup());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnPowerup()
    {
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
                i = Random.Range(0, gameManager.Powerups().Length);
            } while (!gameManager.Powerups()[i]);

            PoolManager.SpawnObject(powerups[i], new(Random.Range(-9f, 9), 5, Random.Range(-9, 9f)), Quaternion.identity);
        }
    }
}
