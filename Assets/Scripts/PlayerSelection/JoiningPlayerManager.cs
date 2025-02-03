using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JoiningPlayerManager : MonoBehaviour
{
    [SerializeField] private float life = 3;
    [SerializeField] private List<GameObject> spawnPoints;
    
    private TextMeshProUGUI textMesh;
    private PlayerMovementController movementController;
    private int playerCount;
    private bool changedScene = true;
    
    private void Awake()
    {
        movementController = GetComponent<PlayerMovementController>();
        playerCount = GameObject.FindGameObjectsWithTag("Player").Length;

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            movementController.enabled = false;
            
            switch (playerCount)
            {
                case 1:
                    transform.position = new(-3, 0, -0.9f);
                    break;
                case 2:
                    transform.position = new(-1, 0, -0.9f);
                    break;
                case 3:
                    transform.position = new(1, 0, -0.9f);
                    break;
                case 4:
                    transform.position = new(3, 0, -0.9f);
                    break;
            }

            DontDestroyOnLoad(this);
        }

        if (gameObject.name == "Player")
        {
            gameObject.name = "Player 1";
        }
        else if (gameObject.name == "Player(Clone)")
        {
            gameObject.name = gameObject.name.Substring(0, gameObject.name.Length - "(clone)".Length) + " " + playerCount;
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex > 1 && changedScene)
        {
            changedScene = false;
            
            movementController.enabled = true;
            
            switch (gameObject.name)
            {
                case "Player 1":
                    transform.position = spawnPoints[0].transform.position;
                    break;
                case "Player 2":
                    transform.position = spawnPoints[1].transform.position;
                    break;
                case "Player 3":
                    transform.position = spawnPoints[2].transform.position;
                    break;
                case "Player 4":
                    transform.position = spawnPoints[3].transform.position;
                    break;
            }
            
            if (GameObject.Find(gameObject.name + " Life").TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI ugui))
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
            
        switch (gameObject.name)
        {
            case "Player 1":
                transform.position = spawnPoints[0].transform.position;
                break;
            case "Player 2":
                transform.position = spawnPoints[1].transform.position;
                break;
            case "Player 3":
                transform.position = spawnPoints[2].transform.position;
                break;
            case "Player 4":
                transform.position = spawnPoints[3].transform.position;
                break;
        }
    }
}
