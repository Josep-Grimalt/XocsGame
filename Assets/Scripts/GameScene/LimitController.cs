using System;
using UnityEngine;

public class LimitController : MonoBehaviour
{
    [SerializeField] private Transform limitCenter;
    [SerializeField] private Transform radiusReference;
    
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 offset = rb.position - limitCenter.position;
        float radius = Vector3.Distance(limitCenter.position, radiusReference.position);
        if (offset.magnitude > radius)
        {
            rb.position = limitCenter.position + offset.normalized * radius;
        }
    }
}
