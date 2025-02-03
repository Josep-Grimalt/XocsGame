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

        if (moving)
        {
            moving = false;

            transform.Translate(transform.forward * (speed * Time.deltaTime));

            if(Vector3.Distance(transform.localPosition, target.position) > 1)
            {
                moving = true;
            }
        }
    }
}
