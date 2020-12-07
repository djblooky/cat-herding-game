using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(MoveController))]
public class Cat : MonoBehaviour
{
    [Tooltip("The minimum distance from the player where the cat will begin to flee")]
    [SerializeField] private float fleeRadius = 1f;
    [SerializeField] private float fleeDuration = 1f;

    private new CircleCollider2D collider;
    private MoveController playerMoveController;
    private MoveController moveController;

    private bool isNearPlayer = false;

    protected void Start()
    {
        moveController = GetComponent<MoveController>();
        collider = GetComponent<CircleCollider2D>();
        collider.radius = fleeRadius;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNearPlayer = true;
            playerMoveController = collision.GetComponent<MoveController>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNearPlayer = false;
        }
    }

    private void Update()
    {
        FleeIfNearPlayer();
    }

    /// <summary>
    /// If the cat is near the player, its move input will be set to the same direction as the player's to flee
    /// </summary>
    protected void FleeIfNearPlayer()
    {
        if (isNearPlayer)
        {
            float xInput = playerMoveController.MoveVector.x;
            float yInput = playerMoveController.MoveVector.y;

            moveController.SetMoveInput(xInput, yInput);

            StartCoroutine(FleeForDuration());
        }
    }

    /// <summary>
    /// Waits for fleeDuration seconds before resetting the cat's move input to stop it from fleeing
    /// </summary>
    private IEnumerator FleeForDuration()
    {
        yield return new WaitForSeconds(fleeDuration);
        moveController.SetMoveInput(0, 0);
    }
}