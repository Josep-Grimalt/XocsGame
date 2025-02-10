using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;

public class PlayerSpawnManager : MonoBehaviour
{
    [SerializeField]
    private Vector3[] selectionSpawnPoints =
    {
        new (-3, 0, -0.9f),
        new(-1, 0, -0.9f),
        new(1, 0, -0.9f),
        new(3, 0, -0.9f)
    };
    [SerializeField]
    private Vector3[] gameSpawnPoints =
    {
        new (-19, 0, 19),
        new (19, 0, 19),
        new (-19, 0, -19),
        new (19, 0, -19)
    };

    private Rigidbody rb;
    private int playerID;

    private void Awake()
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

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            rb.isKinematic = true;

            transform.position = selectionSpawnPoints[playerID];

            GameObject parent = GameObject.Find("Canvas");
            if (parent != null)
            {
                Transform panel = parent.transform.Find("Panel " + (playerID + 1));
                if (panel != null)
                {
                    panel.gameObject.SetActive(true);
                }
            }

            DontDestroyOnLoad(this);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += ChangeSpawnPoint;
    }

    private void ChangeSpawnPoint(Scene arg0, LoadSceneMode arg1)
    {
        if (SceneManager.GetActiveScene().buildIndex > 1)
        {
            rb.isKinematic = false;

            transform.position = gameSpawnPoints[playerID];
        }
    }
}
