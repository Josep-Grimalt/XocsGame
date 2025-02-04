using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class CPUMovementController : MonoBehaviour
{
    private NavMeshAgent agent;
    private List<GameObject> targets;
    private int actualTarget = 0;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        targets = GameObject.FindGameObjectsWithTag("Player").ToList<GameObject>();
        targets.Remove(gameObject);
    }

    private void Update()
    {
        NearPlayer();
        agent.SetDestination(targets[actualTarget].transform.position);
    }

    private void NearPlayer()
    {
        for(int i = 0; i < targets.Count; i++)
        {
            if(Vector3.Distance(transform.position, targets[i].transform.position) < Vector3.Distance(transform.position, targets[actualTarget].transform.position))
            {
                actualTarget = i;
            }
        }
    }
}
