using UnityEngine;

public class PositionChanger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("collision");

        if(other.name.Contains("Wall"))
        {
            CollisionManager.Instance.HandleChangablePosition(gameObject);
        }
    }
}
