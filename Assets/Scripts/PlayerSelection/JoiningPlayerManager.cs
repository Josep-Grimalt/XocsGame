using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class JoiningPlayerManager : MonoBehaviour
{
    [SerializeField] private float life = 3, delay = 2f;
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

    private TextMeshProUGUI textMesh;
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

        //editor only
        if(SceneManager.GetActiveScene().buildIndex > 1)
        {
            transform.position = gameSpawnPoints[playerID];

            if (GameObject.Find(gameObject.name + " Life").TryGetComponent<TextMeshProUGUI>(out var ugui))
            {
                textMesh = ugui;
                textMesh.SetText(gameObject.name + ": " + life);
            }
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
            movementController.enabled = true;

            transform.position = gameSpawnPoints[playerID];

            if (GameObject.Find(gameObject.name + " Life").TryGetComponent<TextMeshProUGUI>(out var ugui))
            {
                textMesh = ugui;
                textMesh.SetText(gameObject.name + ": " + life);
            }
        }
    }

    public void LoseLife()
    {
        life -= 1;

        gameSpawnPoints[playerID] = new (Random.Range(-9, 9), 1, Random.Range(-9, 9));

        
        transform.position = gameSpawnPoints[playerID];

        if (textMesh)
        {
            textMesh.SetText(gameObject.name + ": " + life);
        }

        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }
}
