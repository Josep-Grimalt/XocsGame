using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.AI.Navigation;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameManagerSO manager;
    [SerializeField] private NavMeshSurface surface;
    [SerializeField] private float delay;
    [SerializeField] private GameObject PostGameHUD;
    [SerializeField] private TextMeshProUGUI winnerText;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameObject multiplayerManager;
    [SerializeField] private float speed = 5;
    [SerializeField] private CinemachineCamera cam;

    private List<GameObject> platforms;
    private List<GameObject> players;
    private bool victorySound, gameReady;

    private void Awake()
    {
        gameReady = false;

        if (multiplayerManager)
            multiplayerManager.SetActive(true);

        players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));

        GameObject.Find("Walls").SetActive(manager.Walls());

        if (manager.FallingPlatforms())
        {
            platforms = new List<GameObject>(GameObject.FindGameObjectsWithTag("Floor"));

            StartCoroutine(Fall(delay));
        }

        if (!manager.Elements())
        {
            GameObject.Find("ElementChangers").SetActive(false);
        }

        victorySound = false;
    }

    private void Update()
    {
        foreach (GameObject player in players.ToList())
        {
            if (player.GetComponent<PlayerLifeManager>())
                if (player.GetComponent<PlayerLifeManager>().GetLifes() == 0)
                {
                    players.Remove(player);
                }
        }

        if (players.Count < 2)
        {
            if (PostGameHUD)
                PostGameHUD.SetActive(true);

            if (multiplayerManager)
                multiplayerManager.SetActive(false);

            VictorySound();

            foreach (GameObject player in players)
            {
                player.GetComponent<Rigidbody>().isKinematic = true;
                if (player.TryGetComponent(out CPUMovementController cpu))
                {
                    cpu.enabled = false;
                }

                if (player.GetComponent<PlayerLifeManager>())
                    if (player.GetComponent<PlayerLifeManager>().GetLifes() > 0)
                    {
                        winnerText.text = player.name + " wins!";
                    }
            }

            if (eventSystem)
                eventSystem.SetSelectedGameObject(continueButton);
        }

        if (cam.Priority > 1)
        {
            Invoke(nameof(GameIsReady), 3f);
        }
    }

    private IEnumerator Fall(float delay)
    {
        while (true)
        {
            if (!gameReady) continue;

            yield return new WaitForSeconds(delay);

            int fallingPlat = Random.Range(0, platforms.Count);
            platforms[fallingPlat].GetComponentInChildren<Animator>().Play("Fall");

            yield return new WaitForSeconds(1.1f);

            float timer = 0;

            while (timer < 2)
            {
                platforms[fallingPlat].transform.Translate(Vector3.down * speed * Time.deltaTime);
                timer += Time.deltaTime;
                yield return null;
            }

            Destroy(platforms[fallingPlat]);
            platforms.RemoveAt(fallingPlat);

            if (surface)
                surface.BuildNavMesh();
        }
    }

    public bool[] Powerups()
    {
        return manager.GetPowerups();
    }

    public bool[] Obstacles()
    {
        return manager.GetObstacles();
    }

    public void Continue()
    {
        foreach (GameObject player in players)
        {
            Destroy(player);
        }

        SceneManager.LoadScene(0);
    }

    public bool IsGameReady()
    {
        return gameReady;
    }

    private void VictorySound()
    {
        if (!victorySound)
        {
            victorySound = true;

            if (SoundManager.I)
                SoundManager.I.PlaySound(SoundName.Victory);
        }
    }

    private void GameIsReady()
    {
        if (gameReady) return;

        gameReady = true;
    }
}
