using System;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public static event Action GoalTriggered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cat"))
        {
            Debug.Log("cat collided");
            collision.GetComponent<Cat>().GoalTriggered();
            GoalTriggered?.Invoke();
        }
    }
}
