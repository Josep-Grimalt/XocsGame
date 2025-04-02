using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerCollisionController : MonoBehaviour
{
    [SerializeField] protected Element element = Element.Neutral;
    [SerializeField] protected GameObject[] armor;
    [SerializeField] protected float delay = 5;
    [SerializeField] protected List<Animator> animators;

    public enum Element
    {
        Fire,
        Water,
        Wind,
        Earth,
        Neutral
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
        if (other.gameObject.CompareTag("Element Changer"))
        {
            element = (Element)Random.Range(0, 4);

            SoundManager.I.PlaySound(SoundName.Change);

            switch (element)
            {
                case Element.Fire:
                    armor.ToList().ForEach(a => a.GetComponentsInChildren<Renderer>().ToList().ForEach(r => r.material.color = Color.red));
                    break;
                case Element.Water:
                    armor.ToList().ForEach(a => a.GetComponentsInChildren<Renderer>().ToList().ForEach(r => r.material.color = Color.blue));
                    break;
                case Element.Wind:
                    armor.ToList().ForEach(a => a.GetComponentsInChildren<Renderer>().ToList().ForEach(r => r.material.color = new Color(0.735849f, 0.735849f, 0.735849f, 1)));
                    break;
                case Element.Earth:
                    armor.ToList().ForEach(a => a.GetComponentsInChildren<Renderer>().ToList().ForEach(r => r.material.color = new Color(0.2169811f, 0.1221835f, 0, 1)));
                    break;
            }

            StartCoroutine(ChangerDisable(other.gameObject, delay));
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
}
