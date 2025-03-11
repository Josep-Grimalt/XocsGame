using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


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
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameObject mainFrist, optionFirst, galleryFirst, powerUpDescriptionFirst, matchOptionsFirst;
    [SerializeField] private int startingScene = 2;

    public void StartButton()
    {
        SceneManager.LoadScene("PlayerSelection");
    }

    public void OptionsButton()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
        eventSystem.SetSelectedGameObject(optionFirst);
    }

    public void OptionsBackButton()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        eventSystem.SetSelectedGameObject(mainFrist);
    }

    public void GalleryButton()
    {
        mainMenu.SetActive(false);
        gallery.SetActive(true);
        eventSystem.SetSelectedGameObject(galleryFirst);
    }

    public void GalleryBackButton()
    {
        mainMenu.SetActive(true);
        gallery.SetActive(false);
        eventSystem.SetSelectedGameObject(mainFrist);
    }

    public void PowerUpButton(int index)
    {
        gallery.SetActive(false);
        powerUpDescription.GetComponentsInChildren<Image>()[1].sprite = powerUps[index].sprite;
        powerUpDescription.GetComponentsInChildren<TextMeshProUGUI>()[0].SetText(powerUps[index].name);
        powerUpDescription.GetComponentsInChildren<TextMeshProUGUI>()[1].SetText(powerUps[index].description);
        powerUpDescription.SetActive(true);
        eventSystem.SetSelectedGameObject(powerUpDescriptionFirst);
    }

    public void PowerUpBackButton()
    {
        gallery.SetActive(true);
        powerUpDescription.SetActive(false);
        eventSystem.SetSelectedGameObject(galleryFirst);
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
        eventSystem.SetSelectedGameObject(matchOptionsFirst);
    }
    
    public void StartGameButton()
    {
        SceneManager.LoadScene(startingScene);
    }

    public void BackToPlayerSelection()
    {
        matchOptionsMenu.SetActive(false);
    }

    public void SetStartingScene(int newStartingScene)
    {
        startingScene += newStartingScene;
    }
}
