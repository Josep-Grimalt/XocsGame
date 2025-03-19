using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    [SerializeField] private List<GameObject> fireObjects = new List<GameObject>(8);
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fireObjects = new List<GameObject>(8);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
