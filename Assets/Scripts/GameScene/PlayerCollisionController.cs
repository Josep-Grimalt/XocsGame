using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    [SerializeField] private float repulsiveForce = 10;
    [SerializeField] private GameObject orb;

    private GameObject[] players;

    void OnAwake()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = hit.gameObject.GetComponent<Rigidbody>();
            Debug.Log(rb);
            rb?.AddForce(repulsiveForce * -transform.forward, ForceMode.Impulse);
            
            Instantiate(orb, transform.position, Quaternion.identity);
        }

        if(hit.gameObject.CompareTag("Orb"))
        {
            Destroy(hit.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            Vector3 direction = (other.transform.position - transform.position).normalized;
            direction.y = 0;
            rb.AddForce(direction * repulsiveForce, ForceMode.Impulse);

            StartCoroutine(SpawnOrb(other.GetContact(0).point));
        }

        if(other.gameObject.CompareTag("Orb"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    private IEnumerator SpawnOrb(Vector3 direction)
    {
        yield return new WaitForSeconds(0.18f);

        Instantiate(orb, direction + Vector3.down * 1.5f, Quaternion.identity);
    }
}
