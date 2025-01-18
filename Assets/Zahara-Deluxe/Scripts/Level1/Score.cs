using TMPro;
using UnityEngine;

public class ScoreL1 : MonoBehaviour
{
    public int score = 0;
    public int maxScore;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI maxScoreText;
    // Start is called before the first frame update
    void Start()
    {
        maxScore = PlayerPrefs.GetInt("maxScore", maxScore);
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score.ToString();
        maxScoreText.text = "Max Score: " + maxScore.ToString();

        if (score > maxScore)
        {
            maxScore = score;
            PlayerPrefs.SetInt("maxScore", maxScore);
            PlayerPrefs.Save();
        }
    }
    public void updateScore(int points)
    {
        score += points;
    }
}
