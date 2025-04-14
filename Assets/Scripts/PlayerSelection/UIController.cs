using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private List<ModelMaterialControllerSO> colorsPlayer;
    [SerializeField] private List<GameObject> models;
    [SerializeField] private List<GameObject> materials;
    [SerializeField] private List<Animator> animators;

    private int color = 0;
    private int model = 0;
    private static bool[] _ready = { false, false, false, false };

    void OnEnable()
    {
        SceneManager.activeSceneChanged += ResetReady;
    }

    void OnDisable()
    {
        SceneManager.activeSceneChanged -= ResetReady;
    }

    public void SelectColor()
    {
        if (color == colorsPlayer[model].GetMaterials().Count - 1)
        {
            color = 0;
        }
        else
        {
            color++;
        }

        ChangeColor();
    }

    public void SelectModel()
    {
        if (model == models.Count - 1)
        {
            model = 0;
        }
        else
        {
            model++;
        }

        ChangeModel();
    }

    public int Ready(int playerNum, bool ready)
    {
        _ready[playerNum] = _ready[playerNum] switch
        {
            false when ready => true,
            true when ready => false,
            _ => _ready[playerNum]
        };

        animators[model].SetBool("Ready", _ready[playerNum]);
        return _ready.Count(r => r);
    }

    public int GetModel()
    {
        return model;
    }

    public bool[] GetReady()
    {
        return _ready;
    }

    private void ChangeColor()
    {
        materials[model].GetComponent<Renderer>().material = colorsPlayer[model].GetMaterials()[color];
    }

    private void ChangeModel()
    {
        for (var i = 0; i < models.Count; i++)
        {
            models[i].SetActive(i == model);
        }

        color = 0;
        ChangeColor();
    }

    private void ResetReady(Scene arg0, Scene arg1)
    {
        for (int i = 0; i < _ready.Length; i++)
        {
            _ready[i] = false;
        }
    }

    public void StartMatch()
    {
        SceneManager.LoadScene(3);
    }
}
