using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool fallingPlatform;
    [SerializeField] private bool walls;
    [SerializeField] private float delay;
    private List<GameObject> platforms;

    private void Awake()
    {
        GameObject.Find("Floor").SetActive(!fallingPlatform);
        GameObject.Find("Walls").SetActive(walls);
        if (fallingPlatform)
        {
            platforms = new List<GameObject>(GameObject.FindGameObjectsWithTag("Floor"));

            StartCoroutine(Fall(delay));
        }
    }

    private IEnumerator Fall(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);

            int fallingPlat = UnityEngine.Random.Range(0, platforms.Count);

            Destroy(platforms[fallingPlat].gameObject);
            platforms.RemoveAt(fallingPlat);
        }
    }
}
