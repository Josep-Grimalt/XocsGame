using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerCollisionController : MonoBehaviour
{
    [SerializeField] private Element element = Element.Neutral;
    [SerializeField] private GameObject armor;
    [SerializeField] private float delay = 5;

    public enum Element
    {
        Fire,
        Water,
        Wind,
        Earth,
        Neutral
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Element element2 = other.gameObject.GetComponent<PlayerCollisionController>().GetElement();
            CollisionManager.Instance.HandlePlayerCollision(gameObject, other.gameObject, other.GetContact(0).point, element, element2);
        }

        if (element == Element.Neutral)
        {
            armor.GetComponent<Renderer>().material.color = Color.yellow;
        }

        if (other.gameObject.layer == 3)
        {
            Debug.Log("collision entre player i orb");
            CollisionManager.Instance.HandleOrbCollision(gameObject, other.gameObject, element);
        }

        if (other.gameObject.CompareTag("PowerUp"))
        {
            Debug.Log("Power Up");
            CollisionManager.Instance.HandlePowerUp(gameObject, other.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Element Changer"))
        {
            element = (Element)Random.Range(0, 4);

            switch (element)
            {
                case Element.Fire:
                    armor.GetComponent<Renderer>().material.color = Color.red;
                    break;
                case Element.Water:
                    armor.GetComponent<Renderer>().material.color = Color.blue;
                    break;
                case Element.Wind:
                    armor.GetComponent<Renderer>().material.color = new Color(0.735849f, 0.735849f, 0.735849f, 1);
                    break;
                case Element.Earth:
                    armor.GetComponent<Renderer>().material.color = new Color(0.2169811f, 0.1221835f, 0, 1);
                    break;
            }

            BoxCollider collider = other.gameObject.GetComponent<BoxCollider>();

            StartCoroutine(ChangerDisable(other.gameObject, delay, collider));
        }
    }

    public Element GetElement()
    {
        return element;
    }

    private IEnumerator ChangerDisable(GameObject changer, float delay, BoxCollider collider)
    {
        changer.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        collider.enabled = false;

        yield return new WaitForSeconds(delay);

        changer.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        collider.enabled = true;
    }
}
