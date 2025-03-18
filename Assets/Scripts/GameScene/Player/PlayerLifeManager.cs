using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLifeManager : MonoBehaviour
{
    [SerializeField] private int life = 3;

    private TextMeshProUGUI textMesh;

    void OnEnable()
    {
        SceneManager.activeSceneChanged += SearchForHUDText;

        // EDITOR MODE
        if (SceneManager.GetActiveScene().buildIndex > 1)
        {
            if (GameObject.Find(gameObject.name + " Life").TryGetComponent<TextMeshProUGUI>(out var ugui))
            {
                textMesh = ugui;
                textMesh.SetText(gameObject.name + ": " + life);
            }
        }
    }

    void OnDisable()
    {
        SceneManager.activeSceneChanged -= SearchForHUDText;
    }

    private void SearchForHUDText(Scene arg0, Scene arg1)
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
        SoundManager.I.PlaySound(SoundName.Healing);

        if (textMesh)
        {
            textMesh.SetText(gameObject.name + ": " + life);
        }
    }
}
