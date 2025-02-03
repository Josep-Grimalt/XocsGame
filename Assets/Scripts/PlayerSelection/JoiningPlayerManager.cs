using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class JoiningPlayerManager : MonoBehaviour
{
    [SerializeField] private float life = 3;
    [SerializeField] private Vector3[] _spawnPoints =
    {
        new (-3, 0, -0.9f),
        new(-1, 0, -0.9f),
        new(1, 0, -0.9f),
        new(3, 0, -0.9f)
    };
    [SerializeField] private List<GameObject> spawnPoints;
    
    private TextMeshProUGUI textMesh;
    private PlayerMovementController movementController;
    private bool changedScene = true;
    private int playerID;
    
    private void Awake()
    {
        movementController = GetComponent<PlayerMovementController>();
        playerID = GetComponent<PlayerInput>().playerIndex;

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            movementController.enabled = false;
            
            transform.position = _spawnPoints[playerID];

            DontDestroyOnLoad(this);
        }
        
        gameObject.name = "Player " + playerID + 1;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex > 1 && changedScene)
        {
            changedScene = false;
            
            movementController.enabled = true;
            
            transform.position = spawnPoints[playerID].transform.position;
            
            if (GameObject.Find(gameObject.name + " Life").TryGetComponent<TextMeshProUGUI>(out var ugui))
            {
                textMesh = ugui;
            }
        }

        if (SceneManager.GetActiveScene().buildIndex > 1 && textMesh != null)
        {
            textMesh.SetText(gameObject.name + ": " + life);
        }
    }

    public void LoseLife()
    {
        life -= 1;
            
        transform.position = spawnPoints[playerID].transform.position;
    }
}
