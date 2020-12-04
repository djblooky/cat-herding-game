using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    public Vector2 MoveVector
    {
        get { return moveVector; }
        set
        {
            //update animation state
            moveVector = value;
            CheckForFlip();
        }
    }

    [Header("Customizable properties")]
    [SerializeField] private float moveSpeed = 3f;

    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    private Vector2 moveVector;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        var xInput = Input.GetAxisRaw("Horizontal");
        var yInput = Input.GetAxisRaw("Vertical");

        MoveVector = new Vector2(xInput, yInput);
    }

    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + MoveVector * moveSpeed * Time.fixedDeltaTime);
    }

    private void CheckForFlip()
    {
        if (MoveVector.x == -1)
        {
            spriteRenderer.flipX = true;
        }
        else if (MoveVector.x == 1)
        {
            spriteRenderer.flipX = false;
        }
    }

}
