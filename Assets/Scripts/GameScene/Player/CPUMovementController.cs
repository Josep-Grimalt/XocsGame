using UnityEngine;
using UnityEngine.AI;

public class CPUMovementController : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject[] targets;
    private int actualTarget;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        targets = GameObject.FindGameObjectsWithTag("Player");
    }

    private void Update()
    {
        NearPlayer();
        agent.SetDestination(targets[actualTarget].transform.position);
    }

    private void NearPlayer()
    {
        for(int i = 0; i < targets.Length; i++)
        {
            if(Vector3.Distance(transform.position, targets[i].transform.position) < Vector3.Distance(transform.position, targets[actualTarget].transform.position))
            {
                actualTarget = i;
            }
        }
    }
}
