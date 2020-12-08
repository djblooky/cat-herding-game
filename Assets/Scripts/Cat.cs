using System.Collections;
using UnityEngine;

//[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(MoveController))]
public class Cat : MonoBehaviour
{
    public bool isNearPlayer { get; set; } = false;
    public MoveController playerMoveController;

    [Tooltip("The minimum distance from the player where the cat will begin to flee")]
    [SerializeField] private float sightRadius = 1f;
    [SerializeField] private float fleeDuration = 1f;

    private CircleCollider2D sightCollider;
    private MoveController moveController;
    private bool canMove = true;

    protected void Start()
    {
        moveController = GetComponent<MoveController>();
        sightCollider = GetComponentInChildren<CircleCollider2D>();
        sightCollider.radius = sightRadius;
    }

    private void Update()
    {
        if (canMove)
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

    public void GoalTriggered()
    {
        canMove = false;
        moveController.SetMoveInput(0, 0);
        StopAllCoroutines();
    }
}