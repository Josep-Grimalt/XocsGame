using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class CPUSpawnManager : PlayerSpawnManager
{
    [SerializeField] private NavMeshAgent navMeshAgent;

    private void ChangeSpawnPoint(Scene arg0, LoadSceneMode arg1)
    {
        if (SceneManager.GetActiveScene().buildIndex > 1)
        {
            targets = GameObject.Find("Target Group").GetComponent<CinemachineTargetGroup>();

            if (targets)
            {
                targets.AddMember(gameObject.transform, 0.5f, 1);
            }

            if (animators[0])
                animators[0].SetBool("SceneChange", true);

            if (navMeshAgent)
                navMeshAgent.enabled = true;
            
            transform.position = gameSpawnPoints[playerID];
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
