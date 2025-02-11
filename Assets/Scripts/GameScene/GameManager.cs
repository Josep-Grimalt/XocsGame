using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float delay;
    private List<GameObject> platforms;

    private void Awake()
    {
        GameObject.Find("Walls").SetActive(GameManagerSO.Instance.Walls());
        if (GameManagerSO.Instance.FallingPlatforms())
        {
            platforms = new List<GameObject>(GameObject.FindGameObjectsWithTag("Floor"));

            StartCoroutine(Fall(delay));
        }

        if (!GameManagerSO.Instance.Elements())
        {
            GameObject.Find("ElementChangers").SetActive(false);
        }
    }

    private IEnumerator Fall(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);

            int fallingPlat = UnityEngine.Random.Range(0, platforms.Count);

            Destroy(platforms[fallingPlat]);
            platforms.RemoveAt(fallingPlat);
        }
    }
}
