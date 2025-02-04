using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerCollisionController : MonoBehaviour
{
    public enum Element
    {
        Fire,
        Water,
        Wind,
        Earth,
        Neutral
    }

    [SerializeField] private Element element = Element.Neutral;
    [SerializeField] private GameObject armor;
    [SerializeField] private float delay = 5;
    
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Element element2 = other.gameObject.GetComponent<PlayerCollisionController>().GetElement();
            CollisionManager.Instance.HandlePlayerCollision(gameObject, other.gameObject, other.GetContact(0).point, element, element2);
        }

        if (element == Element.Neutral)
        {
            armor.GetComponent<Renderer>().material.color = Color.yellow;
        }

        if(other.gameObject.layer == 3)
        {
            CollisionManager.Instance.HandleOrbCollision(gameObject, other.gameObject, element);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Element Changer"))
        {
            element = (Element) Random.Range(0, 3);

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

            StartCoroutine(ChangerDisable(other.gameObject, delay));
        }
    }

    public Element GetElement()
    {
        return element;
    }

    private IEnumerator ChangerDisable(GameObject changer, float delay)
    {
        changer.SetActive(false);

        yield return new WaitForSeconds(delay);

        changer.SetActive(true);
    }
}
