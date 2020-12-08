using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    public static int score = 0;

    private TMP_Text textObj;

    private void Start()
    {
        textObj = GetComponent<TMP_Text>();
    }

    private void OnGoalTriggered()
    {
        score++;
        textObj.text = "Score: " + score.ToString();
    }

    private void OnEnable()
    {
        Goal.GoalTriggered += OnGoalTriggered;
    }

    private void OnDisable()
    {
        Goal.GoalTriggered -= OnGoalTriggered;
    }
}
