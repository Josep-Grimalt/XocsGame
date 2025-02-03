using System;
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

        if(other.gameObject.CompareTag("Fire"))
        {
            PoolManager.ReturnObjectToPool(other.gameObject);
            
            if (element != Element.Fire)
            {
                Destroy(gameObject);
            }
        }

        if(other.gameObject.CompareTag("Water"))
        {
            PoolManager.ReturnObjectToPool(other.gameObject);
            
            if (element != Element.Water)
            {
                Destroy(gameObject);
            }
        }

        if(other.gameObject.CompareTag("Wind"))
        {
            PoolManager.ReturnObjectToPool(other.gameObject);
            
            if (element != Element.Wind)
            {
                Destroy(gameObject);
            }
        }

        if(other.gameObject.CompareTag("Earth"))
        {
            PoolManager.ReturnObjectToPool(other.gameObject);
            
            if (element != Element.Earth)
            {
                Destroy(gameObject);
            }
        }

        if(other.gameObject.CompareTag("Neutral"))
        {
            PoolManager.ReturnObjectToPool(other.gameObject);
            Destroy(gameObject);
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
        }
    }

    public Element GetElement()
    {
        return element;
    }
}
