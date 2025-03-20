using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    [SerializeField] private float repulsionforce = 25, speedUpDuration = 1f;
    [SerializeField] private List<GameObject> orb;
    [SerializeField] private float delay = 0.18f, respawnDelay = 3;
    [SerializeField] private GameObject elementChanger;
    [SerializeField] private AddTargetCinemachine targetCinemachine;

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
        SoundManager.I.PlaySound(SoundName.Xoc);

        Vector3 collisionDir = (p2.transform.position - p1.transform.position).normalized;
        collisionDir.y = 0;
        collisionPoint.y = 0.07f;

        Rigidbody rb1 = p1.GetComponent<Rigidbody>();
        Rigidbody rb2 = p2.GetComponent<Rigidbody>();
        if (p1.TryGetComponent(out CPUMovementController cpuP1))
        {
            cpuP1.DisableAgent();
            rb1.isKinematic = false;
        }
        if (p2.TryGetComponent(out CPUMovementController cpuP2))
        {
            cpuP2.DisableAgent();
            rb2.isKinematic = false;
        }
        rb1.AddForce(-collisionDir * repulsionforce, ForceMode.Impulse);
        rb2.AddForce(collisionDir * repulsionforce, ForceMode.Impulse);

        if (cpuP1)
        {
            StartCoroutine(ResetCPU(cpuP1, 2));
        }
        if (cpuP2)
        {
            StartCoroutine(ResetCPU(cpuP2, 2));
        }

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
                SoundManager.I.PlaySound(SoundName.Water, collisionPoint);
                element = PlayerCollisionController.Element.Water;
            }
            else if ((e1 == PlayerCollisionController.Element.Wind && e2 == PlayerCollisionController.Element.Earth) || (e2 == PlayerCollisionController.Element.Wind && e1 == PlayerCollisionController.Element.Earth))
            {
                SoundManager.I.PlaySound(SoundName.Wind, collisionPoint);
                element = PlayerCollisionController.Element.Wind;
            }
            else if ((e1 == PlayerCollisionController.Element.Fire && e2 == PlayerCollisionController.Element.Wind) || (e2 == PlayerCollisionController.Element.Fire && e1 == PlayerCollisionController.Element.Wind))
            {
                SoundManager.I.PlaySound(SoundName.Fire, collisionPoint);
                element = PlayerCollisionController.Element.Fire;
            }
            else if ((e1 == PlayerCollisionController.Element.Earth && e2 == PlayerCollisionController.Element.Water) || (e2 == PlayerCollisionController.Element.Earth && e1 == PlayerCollisionController.Element.Water))
            {
                SoundManager.I.PlaySound(SoundName.Earth, collisionPoint);
                element = PlayerCollisionController.Element.Earth;
            }
            else
            {
                SoundManager.I.PlaySound(SoundName.Neutral, collisionPoint);
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
                StartCoroutine(RespawnPlayer(player, respawnDelay));
                SoundManager.I.PlaySound(SoundName.Death, player.transform);
            }
        }

        if (orb.CompareTag("Water"))
        {
            if (element != PlayerCollisionController.Element.Water)
            {
                StartCoroutine(RespawnPlayer(player, respawnDelay));
                SoundManager.I.PlaySound(SoundName.Death, player.transform);
            }
        }

        if (orb.CompareTag("Wind"))
        {
            if (element != PlayerCollisionController.Element.Wind)
            {
                StartCoroutine(RespawnPlayer(player, respawnDelay));
                SoundManager.I.PlaySound(SoundName.Death, player.transform);
            }
        }

        if (orb.CompareTag("Earth"))
        {
            if (element != PlayerCollisionController.Element.Earth)
            {
                StartCoroutine(RespawnPlayer(player, respawnDelay));
                SoundManager.I.PlaySound(SoundName.Death, player.transform);
            }
        }

        if (orb.CompareTag("Neutral"))
        {
            StartCoroutine(RespawnPlayer(player, respawnDelay));
            SoundManager.I.PlaySound(SoundName.Death, player.transform);
        }

        PoolManager.ReturnObjectToPool(orb);
        StartCoroutine(RemoveCollisionKey(collisionKey, delay));
    }

    public void HandlePowerUp(GameObject player, GameObject powerup)
    {
        switch (powerup.name[..^"(Clone)".Length])
        {
            case "Healing":
                player.GetComponent<PlayerLifeManager>().Heal();
                break;
            case "SpeedUp":
                StartCoroutine(SpeedUp(player));
                break;
        }

        PoolManager.ReturnObjectToPool(powerup);
    }

    private IEnumerator SpeedUp(GameObject player)
    {
        player.GetComponent<PlayerMovementController>().SpeedUp();

        yield return new WaitForSeconds(speedUpDuration);

        player.GetComponent<PlayerMovementController>().SpeedDown();
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
        if (player.TryGetComponent(out CPUMovementController cpu))
        {
            cpu.DisableAgent();
            cpu.Die();
        }

        player.transform.position = new(Random.Range(100, 200), 0, Random.Range(100, 200));
        player.GetComponent<Rigidbody>().isKinematic = true;
        targetCinemachine.RemoveTarget(player);
        player.GetComponent<PlayerLifeManager>().LoseLife();

        yield return new WaitForSeconds(delay);

        if (player.GetComponent<PlayerLifeManager>().GetLifes() > 0)
        {
            player.transform.position = new(Random.Range(-9, 9), 1, Random.Range(-9, 9));
            targetCinemachine.AddTarget(player);
            player.GetComponent<Rigidbody>().isKinematic = false;
            if (cpu)
            {
                cpu.Undie();
                StartCoroutine(ResetCPU(cpu, 0));
            }
        }
        else
            Destroy(player);
    }

    private IEnumerator ResetCPU(CPUMovementController cpu, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (cpu.IsDead()) yield break;

        cpu.EnableAgent();
    }
}