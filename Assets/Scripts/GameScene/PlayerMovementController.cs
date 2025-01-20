using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PlayerMovementController : MonoBehaviour
{
    private CharacterController cc;
    private Rigidbody rb;
    private Vector3 movementDirection;

    [SerializeField] private float speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        //cc = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Fall();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        Vector3 movement = transform.forward * movementDirection.z + transform.right * movementDirection.x;

        movement = Vector3.ClampMagnitude(movement, 1f);

        if (cc is not null)
            cc.Move(movement * speed * Time.deltaTime);

        if (rb is not null)
            rb.AddForce(speed * movement, ForceMode.Force);
    }

    public void SetMovementDirection(Vector2 direction)
    {
        movementDirection = new(direction.x, 0, direction.y);
    }

    private void Fall()
    {
        if (transform.position.y <= -5)
        {
            Destroy(gameObject);
        }
    }
}
