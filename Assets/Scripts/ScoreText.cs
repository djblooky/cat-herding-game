using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreText : MonoBehaviour
{
    public static int score = 0;

    private TMP_Text textObj;

    public GameObject Paw1;
    public GameObject Paw1ALT;
    public GameObject Paw2;
    public GameObject Paw2ALT;
    public GameObject Paw3;
    public GameObject Paw3ALT;
    public GameObject Paw4;
    public GameObject Paw4ALT;
    public GameObject Paw5;
    public GameObject Paw5ALT;

    private void Start()
    {
        textObj = GetComponent<TMP_Text>();
    }

    private void OnGoalTriggered()
    {
        score++;

        textObj.text = "Score: " + score.ToString();

        if (score == 1)
        {
            Paw1.SetActive(false);
            Paw1ALT.SetActive(true);
        }
        if (score == 2)
        {
            Paw2.SetActive(false);
            Paw2ALT.SetActive(true);
        }
        if (score == 3)
        {
            Paw3.SetActive(false);
            Paw3ALT.SetActive(true);
        }
        if (score == 4)
        {
            Debug.Log("4");
            Paw4.SetActive(false);
            Paw4ALT.SetActive(true);
        }
        if (score == 5)
        {
            Debug.Log("5");
            Paw5.SetActive(false);
            Paw5ALT.SetActive(true);
        }

        if (score >= 5)
        {
            Debug.Log("Leave");
            SceneManager.LoadScene("EndScene");
        }

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
