using System;
using UnityEngine;

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

    private void Move()
    {
        Vector3 movement = transform.forward * Math.Abs(movementDirection.z) + transform.forward * Math.Abs(movementDirection.x);

        movement = Vector3.ClampMagnitude(movement, 1f);

        rb?.AddForce(speed * movement, ForceMode.Force);
    }

    public void SetMovementDirection(Vector2 direction)
    {
        movementDirection = new(direction.x, 0, direction.y); ;
    }

    protected void Fall()
    {
        if (transform.position.y <= -5)
        {
            GetComponent<PlayerLifeManager>().LoseLife();
        }
    }

    private void Rotate(Vector3 direction)
    {
        if (direction == Vector3.zero) return;

        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void SpeedUp()
    {
        speed *= 2;
    }

    public void SpeedDown()
    {
        speed /= 2;
    }
}
