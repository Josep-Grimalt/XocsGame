using UnityEngine;

public class ChangerPositionController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.name.Contains("Wall"))
        {
            CollisionManager.Instance.HandleChangablePosition(gameObject);
            Destroy(gameObject);
        }
    }
}
