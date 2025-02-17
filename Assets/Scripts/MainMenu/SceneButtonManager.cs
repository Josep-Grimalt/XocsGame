using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButtonManager : MonoBehaviour
{
    private GameObject mainMenu, optionsMenu ,gallery, matchOptionsMenu;
    
    private void Awake()
    {
        GameObject parent = GameObject.Find("Canvas");
        
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            mainMenu = GameObject.Find("Main Menu");
            
            if (parent != null)
            {
                optionsMenu = parent.transform.Find("Options Menu").gameObject;
                gallery = parent.transform.Find("Gallery").gameObject;
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (parent != null)
            {
                matchOptionsMenu = parent.transform.Find("Match Options Menu").gameObject;
            }
        }
    }

    public void StartButton()
    {
        SceneManager.LoadScene("PlayerSelection");
    }

    public void OptionsButton()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void OptionsBackButton()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void GalleryButton()
    {
        mainMenu.SetActive(false);
        gallery.SetActive(true);
    }

    public void GalleryBackButton()
    {
        mainMenu.SetActive(true);
        gallery.SetActive(false);
    }

    public void PowerUpButton()
    {
        gallery.SetActive(false);
    }

    public void QuitButton()
    {
        // Cierra la aplicación
        Application.Quit();

        // Función extra para el editor (solo útil durante el desarrollo)
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
    
    public void BackButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void StartGameReadyButton()
    {
        matchOptionsMenu.SetActive(true);
    }
    
    public void StartGameButton()
    {
        SceneManager.LoadScene("BasicGameScene");
    }

    public void BackToPlayerSelection()
    {
        matchOptionsMenu.SetActive(false);
    }
}
