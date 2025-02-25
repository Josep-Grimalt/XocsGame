using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PanelGetColorAndGetModel : MonoBehaviour
{
    [SerializeField] private List<GameObject> colors;
    [SerializeField] private TextMeshProUGUI model;

    public List<GameObject> GetColors()
    {
        return colors;
    }

    public TextMeshProUGUI GetModel()
    {
        return model;
    }
}
