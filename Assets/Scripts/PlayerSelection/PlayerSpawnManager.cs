using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerSpawnManager : MonoBehaviour
{
    [SerializeField] protected Vector3[] selectionSpawnPoints =
    {
        new (-3, 0, -0.9f),
        new(-1, 0, -0.9f),
        new(1, 0, -0.9f),
        new(3, 0, -0.9f)
    };
    [SerializeField] protected Vector3[] gameSpawnPoints =
    {
        new (-19, 0, 19),
        new (19, 0, 19),
        new (-19, 0, -19),
        new (19, 0, -19)
    };
    [SerializeField] protected List<Animator> animators;

    protected Rigidbody rb;
    protected int playerID;
    protected CinemachineTargetGroup targets;

    protected void Awake()
    {
        if (TryGetComponent<Rigidbody>(out var i))
        {
            rb = i;
        }

        if (TryGetComponent<PlayerInput>(out var input))
        {
            playerID = input.playerIndex;
            gameObject.name = "Player " + (playerID + 1);
        }
        else
        {
            playerID = int.Parse(name[^1..]) - 1;
        }

        if (SceneManager.GetActiveScene().buildIndex < 3)
        {
            rb.isKinematic = true;

            transform.position = selectionSpawnPoints[playerID];

            DontDestroyOnLoad(this);
        }

        //EDITOR ONLY
        if (SceneManager.GetActiveScene().buildIndex > 3)
        {
            targets = GameObject.Find("Target Group").GetComponent<CinemachineTargetGroup>();

            if (targets)
            {
                targets.AddMember(gameObject.transform, 0.5f, 1);
            }

            transform.position = gameSpawnPoints[playerID];
        }
    }

    protected void OnEnable()
    {
        SceneManager.sceneLoaded += ChangeSpawnPoint;
        SceneManager.sceneLoaded += DestroyPlayer;
    }

    protected void OnDisable()
    {
        SceneManager.sceneLoaded -= ChangeSpawnPoint;
        SceneManager.sceneLoaded -= DestroyPlayer;
    }

    protected virtual void ChangeSpawnPoint(Scene arg0, LoadSceneMode arg1)
    {
        if (SceneManager.GetActiveScene().buildIndex > 3)
        {
            targets = GameObject.Find("Target Group").GetComponent<CinemachineTargetGroup>();

            if (targets)
            {
                targets.AddMember(gameObject.transform, 0.5f, 1);
            }

            if (animators[GetComponent<UIController>().GetModel()])
                animators[GetComponent<UIController>().GetModel()].SetBool("SceneChange", true);
            
            transform.position = gameSpawnPoints[playerID];
            transform.LookAt(Vector3.zero);
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    protected void DestroyPlayer(Scene arg0, LoadSceneMode arg1)
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Destroy(gameObject);
        }
    }
}
