using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerSpawnManager : MonoBehaviour
{
    [SerializeField] private Vector3[] selectionSpawnPoints =
    {
        new (-3, 0, -0.9f),
        new(-1, 0, -0.9f),
        new(1, 0, -0.9f),
        new(3, 0, -0.9f)
    };
    [SerializeField] private Vector3[] gameSpawnPoints =
    {
        new (-19, 0, 19),
        new (19, 0, 19),
        new (-19, 0, -19),
        new (19, 0, -19)
    };

    private PlayerMovementController movementController;
    private int playerID;

    private void Awake()
    {
        movementController = GetComponent<PlayerMovementController>();
        playerID = GetComponent<PlayerInput>().playerIndex;

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            movementController.enabled = false;

            transform.position = selectionSpawnPoints[playerID];

            DontDestroyOnLoad(this);
        }

        gameObject.name = "Player " + (playerID + 1);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += ChangeSpawnPoint;
    }

    private void ChangeSpawnPoint(Scene arg0, LoadSceneMode arg1)
    {
        if (SceneManager.GetActiveScene().buildIndex > 1)
        {
            movementController.enabled = true;

            transform.position = gameSpawnPoints[playerID];
        }
    }
}
