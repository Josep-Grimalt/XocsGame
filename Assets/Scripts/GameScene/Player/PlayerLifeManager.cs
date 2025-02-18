using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class PlayerLifeManager : MonoBehaviour
{
    [SerializeField] private int life = 3;
    [SerializeField]
    private Vector3[] gameSpawnPoints =
    {
        new (-19, 0, 19),
        new (19, 0, 19),
        new (-19, 0, -19),
        new (19, 0, -19)
    };

    private TextMeshProUGUI textMesh;
    private int playerID;

    private void Awake()
    {
        if (TryGetComponent<PlayerInput>(out var input))
        {
            playerID = input.playerIndex;
        }
    }

    void OnEnable()
    {
        if (SceneManager.GetActiveScene().buildIndex > 1)
        {
            if (GameObject.Find(gameObject.name + " Life").TryGetComponent<TextMeshProUGUI>(out var ugui))
            {
                textMesh = ugui;
                textMesh.SetText(gameObject.name + ": " + life);
            }
        }
    }

    public void LoseLife()
    {
        life--;

        gameSpawnPoints[playerID] = new(Random.Range(-9, 9), 1, Random.Range(-9, 9));

        transform.position = gameSpawnPoints[playerID];

        if (textMesh)
        {
            textMesh.SetText(gameObject.name + ": " + life);
        }

        if (life <= 0)
        {
            textMesh.SetText(gameObject.name + ": 0");
            life = 0;
        }
    }

    public int GetLifes()
    {
        return life;
    }

    public void Heal()
    {
        if (life == 3)
        {
            Debug.LogWarning("Vida màxima");
            return;
        }
        life++;

        if (textMesh)
        {
            textMesh.SetText(gameObject.name + ": " + life);
        }
    }
}
