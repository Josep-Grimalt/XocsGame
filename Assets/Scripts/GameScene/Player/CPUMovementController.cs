using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class CPUMovementController : PlayerMovementController
{
    private NavMeshAgent agent;
    private List<GameObject> targets;
    private int actualTarget = 0;
    private bool isDead;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        isDead = false;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1) return;


        Fall();
        NearPlayer();
    }

    private void NearPlayer()
    {
        GetTargets();

        if (targets.Count <= 0 || !agent.enabled) return;

        for (int i = 0; i < targets.Count; i++)
        {
            if (Vector3.Distance(transform.position, targets[i].transform.position) < Vector3.Distance(transform.position, targets[actualTarget].transform.position))
            {
                actualTarget = i;
            }
        }

        agent.SetDestination(targets[actualTarget].transform.position);
    }

    private void GetTargets()
    {
        targets = GameObject.FindGameObjectsWithTag("Player").ToList<GameObject>();
        targets.Remove(gameObject);

        if (actualTarget >= targets.Count)
        {
            actualTarget = 0;
        }
    }

    public void DisableAgent()
    {
        agent.enabled = false;
    }

    public void EnableAgent()
    {
        agent.enabled = true;
    }

    public bool IsDead()
    {
        return isDead;
    }

    public void Die()
    {
        isDead = true;
    }

    public void Undie()
    {
        isDead = false;
    }
}
