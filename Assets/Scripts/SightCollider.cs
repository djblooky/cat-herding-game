using UnityEngine;

public class SightCollider : MonoBehaviour
{
    private Cat cat;

    private void Start()
    {
        cat = GetComponentInParent<Cat>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cat.isNearPlayer = true;
            cat.playerMoveController = collision.GetComponent<MoveController>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cat.isNearPlayer = false;
            cat.canMove = true;
        }
    }
}
