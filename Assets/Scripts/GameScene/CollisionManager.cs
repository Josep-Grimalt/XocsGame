using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    [SerializeField] private float repulsionforce = 25;
    [SerializeField] private List<GameObject> orb;
    [SerializeField] private float delay = 0.18f, respawnDelay = 3;
    [SerializeField] private GameObject elementChanger;

    private HashSet<string> recentCollisions = new HashSet<string>();

    public static CollisionManager Instance { get; private set; }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    public void HandleChangablePosition(GameObject go)
    {
        string collisionKey = go.name;

        if (recentCollisions.Contains(collisionKey)) return;

        recentCollisions.Add(collisionKey);

        Instantiate(elementChanger, Vector3.zero, Quaternion.identity);

        StartCoroutine(RemoveCollisionKey(collisionKey, delay));

    }

    public void HandlePlayerCollision(GameObject p1, GameObject p2, Vector3 collisionPoint, PlayerCollisionController.Element e1, PlayerCollisionController.Element e2)
    {
        string collisionKey = "${p1.GetInstanceID()}-{p2.GetInstanceID()}";

        if (recentCollisions.Contains(collisionKey)) return;

        recentCollisions.Add(collisionKey);

        Vector3 collisionDir = (p2.transform.position - p1.transform.position).normalized;
        collisionDir.y = 0;

        Rigidbody rb1 = p1.GetComponent<Rigidbody>();
        Rigidbody rb2 = p2.GetComponent<Rigidbody>();
        rb1.AddForce(-collisionDir * repulsionforce, ForceMode.Impulse);
        rb2.AddForce(collisionDir * repulsionforce, ForceMode.Impulse);

        PlayerCollisionController.Element element;
        if (e1 == e2)
        {
            element = PlayerCollisionController.Element.Neutral;
        }
        else if (e1 == PlayerCollisionController.Element.Neutral)
        {
            element = e2;
        }
        else if (e2 == PlayerCollisionController.Element.Neutral)
        {
            element = e1;
        }
        else
        {
            if ((e1 == PlayerCollisionController.Element.Fire && e2 == PlayerCollisionController.Element.Water) || (e2 == PlayerCollisionController.Element.Fire && e1 == PlayerCollisionController.Element.Water))
            {
                element = PlayerCollisionController.Element.Water;
            }
            else if ((e1 == PlayerCollisionController.Element.Wind && e2 == PlayerCollisionController.Element.Earth) || (e2 == PlayerCollisionController.Element.Wind && e1 == PlayerCollisionController.Element.Earth))
            {
                element = PlayerCollisionController.Element.Wind;
            }
            else if ((e1 == PlayerCollisionController.Element.Fire && e2 == PlayerCollisionController.Element.Wind) || (e2 == PlayerCollisionController.Element.Fire && e1 == PlayerCollisionController.Element.Wind))
            {
                element = PlayerCollisionController.Element.Fire;
            }
            else if ((e1 == PlayerCollisionController.Element.Earth && e2 == PlayerCollisionController.Element.Water) || (e2 == PlayerCollisionController.Element.Earth && e1 == PlayerCollisionController.Element.Water))
            {
                element = PlayerCollisionController.Element.Earth;
            }
            else
            {
                element = PlayerCollisionController.Element.Neutral;
            }
        }

        StartCoroutine(SpawnOrb(orb[(int)element], collisionPoint, delay));
        StartCoroutine(RemoveCollisionKey(collisionKey, delay));
    }

    public void HandleOrbCollision(GameObject player, GameObject orb, PlayerCollisionController.Element element)
    {
        string collisionKey = "${player.name}-${orb.name}";

        if (recentCollisions.Contains(collisionKey)) return;

        recentCollisions.Add(collisionKey);

        if (orb.CompareTag("Fire"))
        {
            if (element != PlayerCollisionController.Element.Fire)
            {
                player.GetComponent<PlayerLifeManager>().LoseLife();
                StartCoroutine(RespawnPlayer(player, respawnDelay));
            }
        }

        if (orb.CompareTag("Water"))
        {
            if (element != PlayerCollisionController.Element.Water)
            {
                player.GetComponent<PlayerLifeManager>().LoseLife();
                StartCoroutine(RespawnPlayer(player, respawnDelay));
            }
        }

        if (orb.CompareTag("Wind"))
        {
            if (element != PlayerCollisionController.Element.Wind)
            {
                player.GetComponent<PlayerLifeManager>().LoseLife();
                StartCoroutine(RespawnPlayer(player, respawnDelay));
            }
        }

        if (orb.CompareTag("Earth"))
        {
            if (element != PlayerCollisionController.Element.Earth)
            {
                player.GetComponent<PlayerLifeManager>().LoseLife();
                StartCoroutine(RespawnPlayer(player, respawnDelay));
            }
        }

        if (orb.CompareTag("Neutral"))
        {
            player.GetComponent<PlayerLifeManager>().LoseLife();
            StartCoroutine(RespawnPlayer(player, respawnDelay));
        }

        PoolManager.ReturnObjectToPool(orb);
        StartCoroutine(RemoveCollisionKey(collisionKey, delay));
    }

    private IEnumerator SpawnOrb(GameObject orb, Vector3 direction, float delay)
    {
        yield return new WaitForSeconds(delay);

        PoolManager.SpawnObject(orb, direction + Vector3.down * 1.5f, Quaternion.identity);
    }

    private IEnumerator RemoveCollisionKey(string collisionKey, float delay)
    {
        yield return new WaitForSeconds(delay);

        recentCollisions.Remove(collisionKey);
    }

    private IEnumerator RespawnPlayer(GameObject player, float delay)
    {
        player.SetActive(false);

        yield return new WaitForSeconds(delay);

        player.SetActive(true);
    }
}
