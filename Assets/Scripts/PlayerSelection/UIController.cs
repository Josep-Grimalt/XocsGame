using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private List<ModelMaterialControllerSO> colorsPlayer;
    [SerializeField] private List<GameObject> models;
    [SerializeField] private List<GameObject> materials;
    [SerializeField] private List<Avatar> avatars;
    [SerializeField] private Animator animator;

    private int color = 0;
    private int model = 0;
    private static bool[] _ready = { false, false, false, false };

    public void SelectColor(bool next)
    {
        if (next)
        {
            if (color == colorsPlayer[model].GetMaterials().Count - 1)
            {
                color = 0;
            }
            else
            {
                color++;
            }
        }
        else
        {
            if (color == 0)
            {
                color = colorsPlayer[model].GetMaterials().Count - 1;
            }
            else
            {
                color--;
            }
        }

        ChangeColor();
    }

    public void SelectModel(bool next)
    {
        if (next)
        {
            if (model == models.Count - 1)
            {
                model = 0;
            }
            else
            {
                model++;
            }
        }
        else
        {
            if (model == 0)
            {
                model = models.Count - 1;
            }
            else
            {
                model--;
            }
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

        animator.SetBool("Ready", _ready[playerNum]);
        return _ready.Count(r => r);
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

        animator.avatar = avatars[model];
        color = 0;
        ChangeColor();
    }

}
