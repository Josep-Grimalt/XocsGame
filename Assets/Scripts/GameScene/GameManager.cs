using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.AI.Navigation;
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
    private List<GameObject> platforms;
    private List<GameObject> players;
    private bool victorySound;

    private void Awake()
    {
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

            if (player.GetComponent<PlayerLifeManager>().GetLifes() == 0)
            {
                players.Remove(player);
            }

        }

        if (players.Count < 2)
        {
            PostGameHUD.SetActive(true);
            multiplayerManager.SetActive(false);

            VictorySound();

            foreach (GameObject player in players)
            {
                player.GetComponent<Rigidbody>().isKinematic = true;
                if (player.TryGetComponent(out CPUMovementController cpu))
                {
                    cpu.enabled = false;
                }

                if (player.GetComponent<PlayerLifeManager>().GetLifes() > 0)
                {
                    winnerText.text = player.name + " wins!";
                }
            }
            eventSystem.SetSelectedGameObject(continueButton);
        }
    }

    private IEnumerator Fall(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);

            int fallingPlat = Random.Range(0, platforms.Count);

            Destroy(platforms[fallingPlat]);
            platforms.RemoveAt(fallingPlat);
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

    private void VictorySound()
    {
        if (!victorySound)
        {
            victorySound = true;
            SoundManager.I.PlaySound(SoundName.Victory);
        }
    }
}
