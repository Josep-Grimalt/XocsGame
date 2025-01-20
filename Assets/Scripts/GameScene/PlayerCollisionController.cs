using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Player"))
        {
            CollisionManager.Instance.HandlePlayerCollision(gameObject, other.gameObject, other.GetContact(0).point);
        }

        if(other.gameObject.CompareTag("Orb"))
        {
            PoolManager.ReturnObjectToPool(other.gameObject);
            Destroy(gameObject);
        }
    }
}
