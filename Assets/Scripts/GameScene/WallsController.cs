using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsController : MonoBehaviour
{
    [SerializeField] private List<GameObject> walls;
    [SerializeField] private Transform target;
    [SerializeField] private float delay, speed;


    private void Awake()
    {
        StartCoroutine(ShrinkPlatform());
    }

    private IEnumerator ShrinkPlatform()
    {
        yield return new WaitForSeconds(delay);

        Debug.Log("should be moving");

        bool moving = true;

        while (moving)
        {
            moving = false;

            transform.Translate(transform.forward * (speed * Time.deltaTime));

            if(Vector3.Distance(transform.position, target.position) > 1)
            {
                moving = true;
            }
        }
    }
}
