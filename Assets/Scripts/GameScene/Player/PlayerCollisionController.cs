using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerCollisionController : MonoBehaviour
{
    [SerializeField] protected Element element = Element.Neutral;
    [SerializeField] protected GameObject[] armor;
    [SerializeField] protected float delay = 5;
    [SerializeField] protected List<Animator> animators;
    [SerializeField] protected List<Sprite> sprites;

    private Sprite sprite;
    private AddTargetCinemachine targetCinemachine;

    public enum Element
    {
        Fire,
        Water,
        Wind,
        Earth,
        Neutral
    }

    private void Update()
    {
        if (!targetCinemachine)
        {
            targetCinemachine = FindAnyObjectByType<AddTargetCinemachine>();
        }

        if (element == Element.Neutral)
            ChangeElement(4);
    }

    protected virtual void OnCollisionEnter(Collision other)
    {
        // si xoca amb un altre player
        if (other.gameObject.CompareTag("Player"))
        {
            animators[GetComponent<UIController>().GetModel()].SetTrigger("Hit");
            Element element2 = other.gameObject.GetComponent<PlayerCollisionController>().GetElement();
            CollisionManager.Instance.HandlePlayerCollision(gameObject, other.gameObject, other.GetContact(0).point, element, element2);
        }

        // posa l'element neutral a les armadures en comenaçar partida
        if (element == Element.Neutral)
        {
            armor.ToList().ForEach(a => a.GetComponentsInChildren<Renderer>().ToList().ForEach(r => r.material.color = Color.yellow));
        }

        // si xoca amb un orb
        if (other.gameObject.layer == 3)
        {
            CollisionManager.Instance.HandleOrbCollision(gameObject, other.gameObject, element);
        }

        // si interactua amb un power up
        if (other.gameObject.CompareTag("PowerUp"))
        {
            CollisionManager.Instance.HandlePowerUp(gameObject, other.gameObject);
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        // si entra en contacte amb un canviador d'elements
        if (other.gameObject.CompareTag("Element Changer"))
        {
            // selecciona un element aleatòriament
            element = (Element)Random.Range(0, 4);

            // sona el canvi d'element
            SoundManager.I.PlaySound(SoundName.Change);

            // canvia el color de l'armadura i la icona gràfica d'acord amb l'element escollit
            switch (element)
            {
                case Element.Fire:
                    armor.ToList().ForEach(a => a.GetComponentsInChildren<Renderer>().ToList().ForEach(r => r.material.color = Color.red));
                    ChangeElement(0);
                    break;
                case Element.Water:
                    armor.ToList().ForEach(a => a.GetComponentsInChildren<Renderer>().ToList().ForEach(r => r.material.color = Color.blue));
                    ChangeElement(1);
                    break;
                case Element.Wind:
                    armor.ToList().ForEach(a => a.GetComponentsInChildren<Renderer>().ToList().ForEach(r => r.material.color = new Color(0.735849f, 0.735849f, 0.735849f, 1)));
                    ChangeElement(2);
                    break;
                case Element.Earth:
                    armor.ToList().ForEach(a => a.GetComponentsInChildren<Renderer>().ToList().ForEach(r => r.material.color = new Color(0.2169811f, 0.1221835f, 0, 1)));
                    ChangeElement(3);
                    break;
            }

            StartCoroutine(ChangerDisable(other.gameObject, delay));
        }

        if (other.gameObject.CompareTag("Fall"))
        {
            targetCinemachine.RemoveTarget(gameObject);
        }
        if (other.gameObject.CompareTag("Death"))
        {
            StartCoroutine(RespawnPlayer(gameObject, 3));
        }
    }

    public Element GetElement()
    {
        return element;
    }

    protected IEnumerator ChangerDisable(GameObject changer, float delay)
    {
        // desactiva el changer i l'efecte de partícules
        changer.SetActive(false);
        GameObject particles = changer.transform.parent.GetChild(1).gameObject;
        particles.SetActive(false);

        yield return new WaitForSeconds(delay);

        // revisa si el changer encara existeix i si es aixi ho reactiva
        if (changer)
        {
            changer.SetActive(true);
            particles.SetActive(true);
        }
    }

    protected void ChangeElement(int element)
    {
        if (GameObject.Find(gameObject.name + " Life"))
        {
            Image[] images = GameObject.Find(gameObject.name + " Life").GetComponentsInChildren<Image>();
            images[images.Count() - 1].sprite = sprites[element];
        }
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
        player.GetComponent<PlayerLifeManager>().LoseLife();
        player.GetComponent<LimitController>().enabled = false;

        yield return new WaitForSeconds(delay);

        if (player.GetComponent<PlayerLifeManager>().GetLifes() > 0)
        {
            player.transform.position = new(Random.Range(-9, 9), 1, Random.Range(-9, 9));
            targetCinemachine.AddTarget(player);
            player.GetComponent<Rigidbody>().isKinematic = false;
            player.GetComponent<LimitController>().enabled = true;
            if (cpu)
            {
                cpu.Undie();
                StartCoroutine(ResetCPU(cpu, 0));
            }
            else
            {
                player.GetComponent<TextLookAt>().Respawn();
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
