using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Exemple de Singleton
/// </summary>
public class GameManager1 : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public static GameManager1 Instance { get; private set; }
    private List<GameObject> players;

    private void Awake()
    {
        Debug.Log("Game Manager Awake");

        if (Instance && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void AddPlayer(GameObject player)
    {
        players.Add(player);
        Debug.Log(player.name);
        Debug.Log(players.Count);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene 1");
    }
}