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
    private List<GameObject> platforms;
    private List<GameObject> players;

    private void Awake()
    {
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

            foreach (GameObject player in players)
            {
                player.GetComponent<Rigidbody>().isKinematic = true;
                if (player.TryGetComponent(out CPUMovementController cpu))
                {
                    cpu.enabled = false;
                }

                if (player.GetComponent<PlayerLifeManager>().GetLifes() > 0)
                {
                    EventSystem.current.SetSelectedGameObject(continueButton);
                    winnerText.text = player.name + " wins!";
                }
            }
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
        return manager.Powerups();
    }

    public bool[] Obstacles()
    {
        return manager.Obstacles();
    }

    public void Continue()
    {
        foreach (GameObject player in players)
        {
            Destroy(player);
        }

        SceneManager.LoadScene(0);
    }
}
