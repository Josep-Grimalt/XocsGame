using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class TextLookAt : MonoBehaviour
{
    [SerializeField] private Transform textTransform;
    [SerializeField] private TextMeshPro textMesh;
    
    private Camera cam;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;

        if (cam)
            textTransform.transform.LookAt(cam.transform);
        
        textMesh.SetText(gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (cam)
            textTransform.LookAt(cam.transform);
    }
}
