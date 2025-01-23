using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreL1 : MonoBehaviour
{
    public int score = 0;
    public int maxScore;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI maxScoreText;
    public GameObject victoryPanel;
    public GameObject gameOverPanel;
    
    void Start()
    {
        maxScore = PlayerPrefs.GetInt("maxScore", maxScore);
        scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        maxScoreText = GameObject.Find("MaxScore").GetComponent<TextMeshProUGUI>();
        score = 0;
        victoryPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        GameStatusManager.Instance.hasWonGame1 = false;
    }

    void Update()
    {
        scoreText.text = "Puntos: " + score.ToString();
        maxScoreText.text = "Max Puntos: " + maxScore.ToString();

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
    public void EndGame()
    {
        if(score >= 40)
        {
            Won();
        }
        else
        {
            Lose();
        }
    }
    void Won()
    {
        GameStatusManager.Instance.hasWonGame1 = true;
        victoryPanel.SetActive(true);
        Time.timeScale = 0;
    }
    void Lose()
    {
        GameStatusManager.Instance.hasWonGame1 = false;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }
}
