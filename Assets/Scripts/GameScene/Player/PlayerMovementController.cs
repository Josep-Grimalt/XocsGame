using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UIElements;

public class PlayerMovementController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 movementDirection;

    [SerializeField] private float speed = 10;
    [SerializeField] private float rotationSpeed = 45;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Fall();
        Rotate(movementDirection);
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        Vector3 movement = transform.forward * Math.Abs(movementDirection.z) + transform.forward * Math.Abs(movementDirection.x);

        movement = Vector3.ClampMagnitude(movement, 1f);

        rb?.AddForce(speed * movement, ForceMode.Force);
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

    private void Rotate(Vector3 movementDirection)
    {
        if(movementDirection == Vector3.zero) return;

        float angle = Mathf.Atan2(movementDirection.x, movementDirection.z) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
