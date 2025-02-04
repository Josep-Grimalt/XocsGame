using UnityEngine;

public class PositionChanger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger entre: " + other + " / " + gameObject);

        if(other.name.Contains("Wall"))
        {
            CollisionManager.Instance.HandleChangablePosition(gameObject);
            gameObject.SetActive(false);
        }
    }
}
