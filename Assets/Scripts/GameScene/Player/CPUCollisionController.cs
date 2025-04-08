using System.Linq;
using UnityEngine;

public class CPUCollisionController : PlayerCollisionController
{
    protected override void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animators[0].SetTrigger("Hit");
            Element element2 = other.gameObject.GetComponent<PlayerCollisionController>().GetElement();
            CollisionManager.Instance.HandlePlayerCollision(gameObject, other.gameObject, other.GetContact(0).point, element, element2);
        }

        if (element == Element.Neutral)
        {
            armor.ToList().ForEach(a => a.GetComponentsInChildren<Renderer>().ToList().ForEach(r => r.material.color = Color.yellow));
        }

        if (other.gameObject.layer == 4)
        {
            CollisionManager.Instance.HandleOrbCollision(gameObject, other.gameObject, element);
        }

        if (other.gameObject.CompareTag("PowerUp"))
        {
            CollisionManager.Instance.HandlePowerUp(gameObject, other.gameObject);
        }
    }
}
