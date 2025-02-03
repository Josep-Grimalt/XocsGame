using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JoiningPlayerManager : MonoBehaviour
{
    [SerializeField] private float life = 3;
    
    private TextMeshPro textMesh;
    
    private void Awake()
    {
        PlayerMovementController movementController = GetComponent<PlayerMovementController>();
        int playerCount = GameObject.FindGameObjectsWithTag("Player").Length;
        textMesh = GameObject.Find(gameObject.name + " Life").GetComponent<TextMeshPro>();

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
        else
        {
            movementController.enabled = true;
            switch (playerCount)
            {
                case 1:
                    transform.position = new(-19, 0, 19);
                    break;
                case 2:
                    transform.position = new(19, 0, 19);
                    break;
                case 3:
                    transform.position = new(-19, 0, -19);
                    break;
                case 4:
                    transform.position = new(19, 0, -19);
                    break;
            }
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
        throw new NotImplementedException();
    }
}
