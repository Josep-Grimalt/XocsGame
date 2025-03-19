using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    [SerializeField] private List<GameObject> fireObjects;
    [SerializeField] private float minDelay;
    [SerializeField] private float maxDelay = 1f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var fire in fireObjects)
        {
            StartCoroutine(Activate(fire, minDelay, maxDelay));
        }
    }

    private IEnumerator Activate(GameObject fire, float minDelay, float maxDelay)
    {
        yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        
        fire.SetActive(true);
    }
}
