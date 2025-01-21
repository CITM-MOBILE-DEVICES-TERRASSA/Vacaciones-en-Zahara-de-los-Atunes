using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreL1 : MonoBehaviour
{
    public int score = 0;
    public int maxScore;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI maxScoreText;
    public TextMeshProUGUI WonText;
    MenuGame1 menu;
    
    // Start is called before the first frame update
    void Start()
    {
        maxScore = PlayerPrefs.GetInt("maxScore", maxScore);
        score = 0;
        menu = FindObjectOfType<MenuGame1>();
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
    public void EndGame()
    {
        if(score >= 40)
        {
            StartCoroutine(Won());
        }
        else
        {
            StartCoroutine(Reset());
        }
    }
    IEnumerator Won()
    {
        WonText.gameObject.SetActive(true);
        WonText.text = "You Won! \n Score: " + score.ToString();
        yield return new WaitForSecondsRealtime(3);
        menu.GameSelection();
    }
    IEnumerator Reset()
    {
        yield return new WaitForSecondsRealtime(3);
        menu.Restart();
    }
}
