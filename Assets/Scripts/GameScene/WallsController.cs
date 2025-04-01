using UnityEngine;

public class WallsController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float delay, speed;
    [SerializeField] private GameManager gameManager;

    private float time = 0;
    private bool moving = false;

    private void Update()
    {
        if (!gameManager.IsGameReady()) return;

        time += Time.deltaTime;

        if (delay <= time)
        {
            moving = true;
        }

        if (moving && Vector3.Distance(transform.position, target.position) > 10)
        {
            moving = false;

            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
}
