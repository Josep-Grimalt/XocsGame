using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameManagerSO manager;
    [SerializeField] private float delay;
    private List<GameObject> platforms;

    private void Update()
    {
        if(GameObject.FindGameObjectsWithTag("Player").Length < 2)
        {
            SceneManager.LoadScene(0);
        }
    }

    private void Awake()
    {
        GameObject.Find("Walls").SetActive(manager.Walls());
        if (manager.FallingPlatforms())
        {
            platforms = new List<GameObject>(GameObject.FindGameObjectsWithTag("Floor"));

            StartCoroutine(Fall(delay));
        }

        if(!manager.Elements())
        {
            GameObject.Find("ElementChangers").SetActive(false);
        }
    }

    private IEnumerator Fall(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);

            int fallingPlat = Random.Range(0, platforms.Count);

            Destroy(platforms[fallingPlat]);
            platforms.RemoveAt(fallingPlat);
        }
    }

    public bool[] Powerups()
    {
        return manager.Powerups();
    }

    public bool[] Obstacles()
    {
        return manager.Obstacles();
    }
}
