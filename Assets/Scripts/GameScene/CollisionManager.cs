using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    [SerializeField] private float repulsionforce = 25;
    [SerializeField] private GameObject orb;
    [SerializeField] private float delay = 0.18f;

    private HashSet<string> recentCollisions = new HashSet<string>();
    

    public static CollisionManager Instance { get; private set; }

    private void Awake() {
        if(!Instance)
        {
            Instance = this;
        }
    }

    public void HandlePlayerCollision(GameObject p1, GameObject p2, Vector3 collisionPoint)
    {
        string collisionKey = "${p1.GetInstanceID()}-{p2.GetInstanceID()}";

        if(recentCollisions.Contains(collisionKey)) return;

        recentCollisions.Add(collisionKey);

        Vector3 collisionDir = (p2.transform.position - p1.transform.position).normalized;
        collisionDir.y = 0;

        Rigidbody rb1 = p1.GetComponent<Rigidbody>();
        Rigidbody rb2 = p2.GetComponent<Rigidbody>();
        rb1.AddForce(-collisionDir * repulsionforce, ForceMode.Impulse);
        rb2.AddForce(collisionDir * repulsionforce, ForceMode.Impulse);

        StartCoroutine(SpawnOrb(collisionPoint, delay));
        StartCoroutine(RemoveCollisionKey(collisionKey, delay));
    }

    private System.Collections.IEnumerator SpawnOrb(Vector3 direction, float delay)
    {
        yield return new WaitForSeconds(delay);

        PoolManager.SpawnObject(orb, direction + Vector3.down * 1.5f, Quaternion.identity);
    }

    private System.Collections.IEnumerator RemoveCollisionKey(string collisionKey, float delay)
    {
        yield return new WaitForSeconds(delay);

        recentCollisions.Remove(collisionKey);
    }

}
