using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class SceneButtonManager : MonoBehaviour
{
    [Serializable]
    private class PowerUp
    {
        public Sprite sprite;
        public string name;
        public string description;
    }
    
    [SerializeField] private PowerUp[] powerUps;
    [SerializeField] private GameObject mainMenu, optionsMenu, gallery, powerUpDescription, matchOptionsMenu;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameObject mainFrist, optionFirst, galleryFirst, powerUpDescriptionFirst, matchOptionsFirst, playerFirst;
    [SerializeField] private PlayerInputManager inputManager;
    [SerializeField] private int scenesCount = 2;
    [SerializeField] private GameManagerSO gameManagerSO;

    void Awake()
    {
        SetRandomStartingScene(scenesCount);
    }

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
        inputManager.DisableJoining();

        GameObject[] buttons = GameObject.FindGameObjectsWithTag("Ready");

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Button>().interactable = false;
        }
    }

    public void BackToPlayerSelection()
    {
        matchOptionsMenu.SetActive(false);
        eventSystem.SetSelectedGameObject(playerFirst);
        inputManager.EnableJoining();

        GameObject[] buttons = GameObject.FindGameObjectsWithTag("Ready");

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Button>().interactable = true;
        }
    }

    public void StartGameButton()
    {
        SceneManager.LoadScene(2);
    }

    public void SetStartingScene(int newStartingScene)
    {
        gameManagerSO.startingScene = newStartingScene + 3;
    }

    public void SetRandomStartingScene(int scenesCount)
    {
        gameManagerSO.startingScene = Random.Range(0, scenesCount) + 3;
    }
}
