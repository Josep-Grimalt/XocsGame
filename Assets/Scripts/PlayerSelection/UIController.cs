using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private List<GameObject> colorsPlayer;
    [SerializeField] private List<GameObject> models;
    private int color = 1;
    private int model = 1;
    private static bool[] _ready = { false, false, false, false };

    public void SelectColor(bool next)
    {
        if (next)
        {
            if (color == 12)
            {
                color = 1;
            }
            else
            {
                color++;
            }
        }
        else
        {
            if (color == 1)
            {
                color = 12;
            }
            else
            {
                color--;
            }
        }

        ChangeColor();
    }

    public string SelectModel(bool next)
    {
        if (next)
        {
            if (model == 2)
            {
                model = 1;
            }
            else
            {
                model++;
            }
        }
        else
        {
            if (model == 1)
            {
                model = 2;
            }
            else
            {
                model--;
            }
        }

        ChangeModel();
        
        return models[model].name;
    }

    public int Ready(int playerNum, bool ready)
    {
        if (!_ready[playerNum] && ready)
        {
            _ready[playerNum] = true;
        }
        else if (_ready[playerNum] && ready)
        {
            _ready[playerNum] = false;
        }

        int num = 0;

        foreach (bool r in _ready)
        {
            if (r)
            {
                num++;
            }
        }
        
        Debug.Log("p1: " + _ready[0]);
        Debug.Log("p2: " + _ready[1]);
        Debug.Log("p3: " + _ready[2]);
        Debug.Log("p4: " + _ready[3]);
        Debug.Log("total: " + num);
        
        return num;
    }

    public bool[] GetReady()
    {
        return _ready;
    }

    private void ChangeColor()
    {
        switch (color)
        {
            case 1:
                foreach (GameObject colorOject in colorsPlayer)
                {
                    if (colorOject.name == "Color 1")
                    {
                        colorOject.SetActive(true);
                    }
                    else
                    {
                        colorOject.SetActive(false);
                    }
                }
                break;

            case 2:
                foreach (GameObject colorOject in colorsPlayer)
                {
                    if (colorOject.name == "Color 2")
                    {
                        colorOject.SetActive(true);
                    }
                    else
                    {
                        colorOject.SetActive(false);
                    }
                }
                break;

            case 3:
                foreach (GameObject colorOject in colorsPlayer)
                {
                    if (colorOject.name == "Color 3")
                    {
                        colorOject.SetActive(true);
                    }
                    else
                    {
                        colorOject.SetActive(false);
                    }
                }
                break;

            case 4:
                foreach (GameObject colorOject in colorsPlayer)
                {
                    if (colorOject.name == "Color 4")
                    {
                        colorOject.SetActive(true);
                    }
                    else
                    {
                        colorOject.SetActive(false);
                    }
                }
                break;

            case 5:
                foreach (GameObject colorOject in colorsPlayer)
                {
                    if (colorOject.name == "Color 5")
                    {
                        colorOject.SetActive(true);
                    }
                    else
                    {
                        colorOject.SetActive(false);
                    }
                }
                break;

            case 6:
                foreach (GameObject colorOject in colorsPlayer)
                {
                    if (colorOject.name == "Color 6")
                    {
                        colorOject.SetActive(true);
                    }
                    else
                    {
                        colorOject.SetActive(false);
                    }
                }
                break;
            case 7:
                foreach (GameObject colorOject in colorsPlayer)
                {
                    if (colorOject.name == "Color 7")
                    {
                        colorOject.SetActive(true);
                    }
                    else
                    {
                        colorOject.SetActive(false);
                    }
                }
                break;

            case 8:
                foreach (GameObject colorOject in colorsPlayer)
                {
                    if (colorOject.name == "Color 8")
                    {
                        colorOject.SetActive(true);
                    }
                    else
                    {
                        colorOject.SetActive(false);
                    }
                }
                break;

            case 9:
                foreach (GameObject colorOject in colorsPlayer)
                {
                    if (colorOject.name == "Color 9")
                    {
                        colorOject.SetActive(true);
                    }
                    else
                    {
                        colorOject.SetActive(false);
                    }
                }
                break;

            case 10:
                foreach (GameObject colorOject in colorsPlayer)
                {
                    if (colorOject.name == "Color 10")
                    {
                        colorOject.SetActive(true);
                    }
                    else
                    {
                        colorOject.SetActive(false);
                    }
                }
                break;

            case 11:
                foreach (GameObject colorOject in colorsPlayer)
                {
                    if (colorOject.name == "Color 11")
                    {
                        colorOject.SetActive(true);
                    }
                    else
                    {
                        colorOject.SetActive(false);
                    }
                }
                break;

            case 12:
                foreach (GameObject colorOject in colorsPlayer)
                {
                    if (colorOject.name == "Color 12")
                    {
                        colorOject.SetActive(true);
                    }
                    else
                    {
                        colorOject.SetActive(false);
                    }
                }
                break;
        }
    }

    private void ChangeModel()
    {
        switch (model)
        {
            case 1:
                foreach (GameObject modelObject in models)
                {
                    if (modelObject.name == "Model 1")
                    {
                        modelObject.SetActive(true);
                    }
                    else
                    {
                        modelObject.SetActive(false);
                    }
                }
                break;

            case 2:
                foreach (GameObject modelObject in models)
                {
                    if (modelObject.name == "Model 2")
                    {
                        modelObject.SetActive(true);
                    }
                    else
                    {
                        modelObject.SetActive(false);
                    }
                }
                break;
        }
    }
}
