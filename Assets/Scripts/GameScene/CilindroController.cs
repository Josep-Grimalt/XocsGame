using UnityEngine;

public class CilindroController : MonoBehaviour
{
    [SerializeField] private float delay = 1f;
    [SerializeField] private Vector3 reduction = new(-0.5f, 0f, -0.5f);

    private float time = 0;

    private void Update()
    {
        time += Time.deltaTime;

        if (time >= delay)
        {
            Reduction();

            time = 0;
        }
    }

    private void Reduction()
    {
        if (transform.localScale.x > 15f && transform.localScale.z > 15f)
        {
            transform.localScale += reduction;
        }
    }
}