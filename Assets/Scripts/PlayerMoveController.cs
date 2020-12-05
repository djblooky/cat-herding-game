using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    public Vector2 MoveVector
    {
        get { return moveVector; }
        set
        {
            moveVector = value;
            UpdateMoveAnimParams();
            CheckForFlip();
        }
    }

    [SerializeField] private float moveSpeed = 3f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector2 moveVector;
    private Animator animator;

    private readonly int verticalAnimParam = Animator.StringToHash("vertical");
    private readonly int horizontalAnimParam = Animator.StringToHash("horizontal");
    private readonly int isMovingAnimParam = Animator.StringToHash("isMoving");

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        var xInput = Input.GetAxisRaw("Horizontal");
        var yInput = Input.GetAxisRaw("Vertical");

        MoveVector = new Vector2(xInput, yInput);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + MoveVector * moveSpeed * Time.fixedDeltaTime);
    }

    /// <summary>
    /// Flips the sprite on the X Axis if it is moving left, because the horizontal sprite faces right by default
    /// </summary>
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

    private void UpdateMoveAnimParams()
    {
        animator.SetFloat(horizontalAnimParam, MoveVector.x);
        animator.SetFloat(verticalAnimParam, MoveVector.y);
        animator.SetBool(isMovingAnimParam, MoveVector.sqrMagnitude > 0);
    }

}
