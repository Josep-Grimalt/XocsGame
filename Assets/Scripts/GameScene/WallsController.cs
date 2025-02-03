using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float delay, speed;

    private float time = 0;
    private bool moving = false;

    private void Update()
    {
        time += Time.deltaTime;
        
        if (delay <= time)
        {
            moving = true;
        }

        if (moving && Vector3.Distance(transform.position, target.position) > 5)
        {
            moving = false;

            // transform.Translate(transform.forward * (speed * Time.deltaTime));
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
}
