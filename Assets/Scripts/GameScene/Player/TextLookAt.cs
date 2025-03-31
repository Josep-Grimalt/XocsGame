using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    void OnEnable()
    {
        SceneManager.activeSceneChanged += ActivateVanishing;
    }

    // Update is called once per frame
    void Update()
    {
        cam = Camera.main;

        if (cam)
            textTransform.LookAt(cam.transform);
    }

    void OnDisable()
    {

    }

    private void ActivateVanishing(Scene arg0, Scene arg1)
    {
        if (SceneManager.GetActiveScene().buildIndex < 2) return;

        Invoke(nameof(Deactivate), 3f);
    }

    private void Deactivate()
    {
        textMesh.SetText("");
    }

    public void Respawn()
    {
        textMesh.SetText(gameObject.name);
        Invoke(nameof(Deactivate), 3f);
    }
}
