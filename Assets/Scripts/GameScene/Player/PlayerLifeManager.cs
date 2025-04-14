using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLifeManager : MonoBehaviour
{
    [SerializeField] private int life = 3;
    [SerializeField] private int maxLife = 3;
    [SerializeField] private List<Sprite> hearts;

    private Transform playerLife;

    void OnEnable()
    {
        SceneManager.activeSceneChanged += SearchForHUDText;

        // EDITOR MODE
        if (SceneManager.GetActiveScene().buildIndex > 3)
        {
            if (GameObject.Find(gameObject.name + " Life"))
            {
                playerLife = GameObject.Find(gameObject.name + " Life").transform;
            }
        }
    }

    void OnDisable()
    {
        SceneManager.activeSceneChanged -= SearchForHUDText;
    }

    private void SearchForHUDText(Scene arg0, Scene arg1)
    {
        if (SceneManager.GetActiveScene().buildIndex > 3)
        {
            if (GameObject.Find(gameObject.name + " Life"))
            {
                playerLife = GameObject.Find(gameObject.name + " Life").transform;
            }
        }
    }

    public void LoseLife()
    {
        life--;

        if (life <= 0)
        {
            life = 0;
        }

        playerLife.GetChild(1).GetChild(life).GetComponent<UnityEngine.UI.Image>().sprite = hearts[1];
    }

    public int GetLifes()
    {
        return life;
    }

    public void Heal()
    {
        if (life == maxLife)
        {
            Debug.LogWarning("Vida màxima");
            return;
        }

        playerLife.GetChild(1).GetChild(life).GetComponent<UnityEngine.UI.Image>().sprite = hearts[0];

        life++;
        SoundManager.I.PlaySound(SoundName.Healing, transform);
    }
}
