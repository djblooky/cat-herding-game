using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 3f;

    private Rigidbody2D rigidbody;
    private Vector2 movementVector;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //handle input
        movementVector.x = Input.GetAxisRaw("Horizontal");
        movementVector.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        //handle movement
        rigidbody.MovePosition(rigidbody.position + movementVector * moveSpeed * Time.fixedDeltaTime);
    }
}
