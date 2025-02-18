using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SceneButtonManager : MonoBehaviour
{
    [Serializable] private class PowerUp
    {
        public Sprite sprite;
        public string name;
        public string description;
    }
    
    [SerializeField] private PowerUp[] powerUps;
    
    [SerializeField] private GameObject mainMenu, optionsMenu ,gallery, powerUpDescription, matchOptionsMenu;

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

    public void PowerUpButton(int index)
    {
        gallery.SetActive(false);
        powerUpDescription.GetComponentInChildren<Image>().sprite = powerUps[index].sprite;
        powerUpDescription.GetComponentsInChildren<TextMeshProUGUI>()[0].SetText(powerUps[index].name);
        powerUpDescription.GetComponentsInChildren<TextMeshProUGUI>()[1].SetText(powerUps[index].description);
        powerUpDescription.SetActive(true);
    }

    public void PowerUpBackButton()
    {
        gallery.SetActive(true);
        powerUpDescription.SetActive(false);
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
