using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float rotationSpeed = 45;
    [SerializeField] protected List<Animator> animators;

    protected GameManager gameManager;
    private Rigidbody rb;
    private Vector3 movementDirection;
    protected AddTargetCinemachine targetCinemachine;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!targetCinemachine && SceneManager.GetActiveScene().buildIndex > 3)
        {
            targetCinemachine = FindAnyObjectByType<AddTargetCinemachine>();
        }
        animators[GetComponent<UIController>().GetModel()].SetFloat("Speed", rb.linearVelocity.magnitude);
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
        if (SceneManager.GetActiveScene().buildIndex == 2) return;

        if (!gameManager)
        {
            gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        }
        if (!gameManager.IsGameReady()) return;

        movementDirection = new(direction.x, 0, direction.y);
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
        SoundManager.I.PlaySound(SoundName.Speed, transform);
        speed *= 2;
    }

    public void SpeedDown()
    {
        speed /= 2;
    }
}
