using System.Linq;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JoiningPlayerManager : MonoBehaviour
{
    private void Awake()
    {
        PlayerMovementController movementController = GetComponent<PlayerMovementController>();
        int playerCount = GameObject.FindGameObjectsWithTag("Player").Length;

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
    }
}
