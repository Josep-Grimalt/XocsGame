using TMPro;
using UnityEngine;

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
        cam = Camera.main;
        
        if (cam)
            textTransform.LookAt(cam.transform);
    }
}
