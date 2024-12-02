using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    [SerializeField] private float repulsiveForce = 10;
    [SerializeField] private Rigidbody selfRb;
    [SerializeField] private GameObject orb;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();

            rb.AddForce(-transform.forward * repulsiveForce, ForceMode.Impulse);
            selfRb.AddForce(-transform.forward * repulsiveForce, ForceMode.Impulse);

            Instantiate(orb, transform.position, Quaternion.identity);
        }

        if (other.gameObject.CompareTag("Orb"))
        {
            //Destroy(gameObject);
            //Destroy(other.gameObject);
        }
    }
}
