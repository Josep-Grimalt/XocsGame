using UnityEngine;

public class CilindroController : MonoBehaviour
{
    [SerializeField] private float delay;
    [SerializeField] private Vector3 reduction = new(-.1f, 0f, -.1f);

    private float time = 0;
    private bool moving = false;

    private void Update()
    {
        time += Time.deltaTime;

        if (delay <= time)
        {
            moving = true;
        }

        if (moving && (transform.localScale.x < 10 || transform.localScale.z < 10))
        {
            moving = false;

            transform.localScale -= reduction;
        }
    }
}
