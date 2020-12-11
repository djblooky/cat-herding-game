using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class MoveController : MonoBehaviour
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

    public float moveSpeed = 1f;

    // Components
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // Reference variable to access the parameters in the Animator\
    private readonly int verticalAnimParam = Animator.StringToHash("vertical");
    private readonly int horizontalAnimParam = Animator.StringToHash("horizontal");
    private readonly int isMovingAnimParam = Animator.StringToHash("isMoving");

    // Move input variables
    private float xInput = 0, yInput = 0;
    private Vector2 moveVector; //do not use, instead use capital MoveVector

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    protected void Update()
    {
        UpdateMoveVector();
    }

    /// <summary>
    /// A setter to allow Character classes to set the move input (xInput and yInput)
    /// </summary>
    public void SetMoveInput(float x, float y)
    {
        xInput = x;
        yInput = y;
    }

    /// <summary>
    /// Sets the Vector2 MoveVector based off of the latest xInput and yInput
    /// </summary>
    private void UpdateMoveVector()
    {
        MoveVector = new Vector2(xInput, yInput);
    }

    protected virtual void FixedUpdate()
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