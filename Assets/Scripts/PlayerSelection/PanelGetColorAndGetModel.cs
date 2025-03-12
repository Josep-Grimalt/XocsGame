using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PanelGetColorAndGetModel : MonoBehaviour
{
    [SerializeField] private List<Material> colors;
    [SerializeField] private TextMeshProUGUI model;

    public List<Material> GetColors()
    {
        return colors;
    }

    public TextMeshProUGUI GetModel()
    {
        return model;
    }
}
