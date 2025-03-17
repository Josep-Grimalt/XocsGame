using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ModelMaterialController", menuName = "SCO/ModelMaterialController")]
public class ModelMaterialControllerSO : ScriptableObject
{
    [SerializeField] private List<Material> materials;

    public List<Material> GetMaterials()
    {
        return materials;
    }
}
