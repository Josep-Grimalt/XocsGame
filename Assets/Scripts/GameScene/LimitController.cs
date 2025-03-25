using UnityEngine;
using UnityEngine.SceneManagement;

public class LimitController : MonoBehaviour
{
    private Transform limitCenter;
    private Transform radiusReference;

    private Rigidbody rb;

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += SetLimits;
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= SetLimits;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
        if (SceneManager.GetActiveScene().buildIndex > 1)
        {
            limitCenter = GameObject.FindGameObjectWithTag("Limit").transform;
            radiusReference = GameObject.FindGameObjectWithTag("RadRef").transform;
        }
    }

    void FixedUpdate()
    {
        Vector3 offset = rb.position - limitCenter.position;
        float radius = Vector3.Distance(limitCenter.position, radiusReference.position);
        if (offset.magnitude > radius)
        {
            rb.position = limitCenter.position + offset.normalized * radius;
        }
    }

    private void SetLimits(Scene arg0, Scene arg1)
    {
        if (SceneManager.GetActiveScene().buildIndex > 1)
        {
            limitCenter = GameObject.FindGameObjectWithTag("Limit").transform;
            radiusReference = GameObject.FindGameObjectWithTag("RadRef").transform;
        }
    }
}
