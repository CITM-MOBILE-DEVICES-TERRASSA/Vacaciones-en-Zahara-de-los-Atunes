using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UpdateLobbyScore : MonoBehaviour
{
    public TextMeshProUGUI lobbyScore1;
    public TextMeshProUGUI lobbyScore2;
    private int score1 = 0;
    private int score2 = 0;
    private ScoreManager scoreManager;

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }
    private void Update()
    {
        ShowScoreText1();
        ShowScoreText2();
    }

    public void UpdateScoreText1(int points)
    {
        score1 = points;
    }
    public void UpdateScoreText2(int points)
    {
        score2 = points;
    }
    private void ShowScoreText1()
    {
        int score = PlayerPrefs.GetInt("Level1Score", score1); 
        lobbyScore1.text = score.ToString();
    }
    private void ShowScoreText2()
    {
        int score = PlayerPrefs.GetInt("PlayerScore", score2); 
        lobbyScore1.text = score.ToString();
    }
}
