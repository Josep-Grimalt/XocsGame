using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButtonManager : MonoBehaviour
{
    private GameObject mainMenu, optionsMenu ,gallery, backButton, startButton, matchOptionsMenu;
    
    private void Awake()
    {
        mainMenu = GameObject.Find("Main Menu");
        optionsMenu = GameObject.Find("Options Menu");
        optionsMenu.SetActive(false);
        gallery = GameObject.Find("Gallery");
        // gallery.SetActive(false);
        backButton = GameObject.Find("BackButton");
        startButton = GameObject.Find("StartButton");
        matchOptionsMenu = GameObject.Find("Match Options Menu");
        matchOptionsMenu.SetActive(false);
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
        backButton.SetActive(false);
        startButton.SetActive(false);
        matchOptionsMenu.SetActive(true);
    }
}
