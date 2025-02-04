using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool fallingPlatform;
    [SerializeField] private bool walls;
    [SerializeField] private float delay;
    [SerializeField] private int CPUs = 4;
    [SerializeField] private GameObject CPUPlayer;
    private List<GameObject> platforms;

    private void Awake()
    {
        GameObject.Find("Walls").SetActive(walls);
        if (fallingPlatform)
        {
            platforms = new List<GameObject>(GameObject.FindGameObjectsWithTag("Floor"));

            StartCoroutine(Fall(delay));
        }

        if (CPUs > 4) CPUs = 4;
        for (int i = 0; i < CPUs; i++)
        {
            Instantiate(CPUPlayer);
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
