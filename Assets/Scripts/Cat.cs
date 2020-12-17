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

    [SerializeField]
    private CircleCollider2D sightCollider;
    private MoveController moveController;
    private bool canMove = true;

    //Dwight Code Zone // For The borders of the cat movement zone
    [SerializeField] private GameObject TopLeftBorder;
    [SerializeField] private GameObject TopRightBorder;
    [SerializeField] private GameObject BottomLeftBorder;
    [SerializeField] private GameObject BottomRightBorder;
    
    private bool HasInteractedWithPlayer = false;
    private bool ReadyToMoveAgain = true;
    private bool JustHitTop = false;
    private bool JustHitBottom = false;
    private bool JustHitLeft = false;
    private bool JustHitRight = false;

    protected void Start()
    {
        moveController = GetComponent<MoveController>();
        sightCollider.radius = sightRadius;
    }

    private void Update()
    {

        if (!HasInteractedWithPlayer && ReadyToMoveAgain && canMove) // Dwight
            WalkAround();

        if (canMove)
            FleeIfNearPlayer();

        if (!ReadyToMoveAgain && !HasInteractedWithPlayer && canMove)
            BoundChecker();
    }

    /// <summary>
    /// If the cat is near the player, its move input will be set to the same direction as the player's to flee
    /// </summary>
    protected void FleeIfNearPlayer()
    {
        if (isNearPlayer)
        {
            //moveController.SetMoveInput(0, 0);
            moveController.moveSpeed = 3f;

            Vector2 towards = transform.position - playerMoveController.gameObject.transform.position;

            float xInput = towards.x;
            float yInput = towards.y;
            //float xInput = playerMoveController.MoveVector.x;
            //float yInput = playerMoveController.MoveVector.y;

            HasInteractedWithPlayer = true; // Dwight

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
        //moveController.SetMoveInput(0, 0);
        Invoke("StopAfterShortTime", .5f);
        StopAllCoroutines();
    }

    public void WalkAround() // Dwight
    {

        ReadyToMoveAgain = false;

        int RandX = Random.Range(-1, 2);
        int RandY = Random.Range(-1, 2);

        if (!JustHitBottom && !JustHitTop && !JustHitRight && !JustHitLeft)
            Invoke("StopAfterShortTime", 1.5f);

        if (JustHitRight)
        {
            RandX = -1;
            JustHitRight = false;
        }
        if (JustHitLeft)
        {
            RandX = 1;
            JustHitLeft = false;
        }
        if (JustHitTop)
        {
            RandY = -1;
            JustHitTop = false;
        }
        if (JustHitBottom)
        {
            RandY = 1;
            JustHitBottom = false;
        }

        moveController.SetMoveInput(RandX, RandY);

    }

    public void BoundChecker()
    {

        if (gameObject.transform.position.x >= TopRightBorder.transform.position.x || gameObject.transform.position.x >= BottomRightBorder.transform.position.x)
        {
            moveController.SetMoveInput(0, 0);
            gameObject.transform.Translate(new Vector2(-0.01f, 0));
            //Invoke("ResetWalker", fleeDuration);
            StopAfterShortTime();
            JustHitRight = true;
        }
        if (gameObject.transform.position.x <= TopLeftBorder.transform.position.x || gameObject.transform.position.x <= BottomLeftBorder.transform.position.x)
        {
            moveController.SetMoveInput(0, 0);
            gameObject.transform.Translate(new Vector2(0.01f, 0));
            //Invoke("ResetWalker", fleeDuration);
            StopAfterShortTime();
            JustHitLeft = true;
        }
        if (gameObject.transform.position.y >= TopRightBorder.transform.position.y || gameObject.transform.position.y >= TopLeftBorder.transform.position.y)
        {
            moveController.SetMoveInput(0, 0);
            gameObject.transform.Translate(new Vector2(0, -0.01f));
            //Invoke("ResetWalker", fleeDuration);
            StopAfterShortTime();
            JustHitTop = true;
        }
        if (gameObject.transform.position.y <= BottomRightBorder.transform.position.y || gameObject.transform.position.y <= BottomLeftBorder.transform.position.y)
        {
            moveController.SetMoveInput(0, 0);
            gameObject.transform.Translate(new Vector2(0, 0.01f));
            //Invoke("ResetWalker", fleeDuration);
            StopAfterShortTime();
            JustHitBottom = true;
        }

    }

    public void ResetWalker()
    {

        ReadyToMoveAgain = true;

    }

    public void StopAfterShortTime()
    {

        moveController.SetMoveInput(0, 0);
        Invoke("ResetWalker", 5f);
        //ResetWalker();

    }

}