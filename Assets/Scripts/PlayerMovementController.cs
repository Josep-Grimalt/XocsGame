using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private CharacterController cc;
    private Vector3 movementDirection;
    
    [SerializeField] private float speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    private void Update() {
        Move();
    }

    public void Move()
    {
        Vector3 movement = transform.forward * movementDirection.z + transform.right * movementDirection.x;
        
        movement = Vector3.ClampMagnitude(movement, 1f);

        cc.Move(movement * speed * Time.deltaTime);
    }

    public void SetMovementDirection(Vector2 direction)
    {
        movementDirection = new (direction.x, 0, direction.y);
    }
}
