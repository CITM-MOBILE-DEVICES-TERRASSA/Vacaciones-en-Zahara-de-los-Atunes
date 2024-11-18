using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UpdateLobbyScore : MonoBehaviour
{
    public TextMeshProUGUI lobbyScore1;
    //public TextMeshProUGUI lobbyScore2;
    private int totalscore = 0;
    
    private ScoreManager scoreManager;

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }
    private void Update()
    {
        ShowScoreText1();
    }
    public void UpdateTotalScoreText(int points)
    {
        totalscore += points;
    }
     private void ShowScoreText1()
    {
        int score = PlayerPrefs.GetInt("Level1Score", totalscore); 
        lobbyScore1.text = score.ToString();
    }
}
