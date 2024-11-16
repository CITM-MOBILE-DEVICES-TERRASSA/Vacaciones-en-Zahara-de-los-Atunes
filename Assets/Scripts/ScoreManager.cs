using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    #region Singleton
    private static ScoreManager _instance;
    public static ScoreManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }
    #endregion


    private int score;
    private int totalScore;

    private int Maxscore1;
    private int MaxScore2;
    private UpdateLobbyScore lobbyscore;
    void Start()
    {
        score = PlayerPrefs.GetInt("GameScore");
        totalScore = PlayerPrefs.GetInt("TotalScore");
        lobbyscore = FindObjectOfType<UpdateLobbyScore>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            
        }
    }

    public void UpdateMaxScore1(int points)
    {
        Maxscore1 = points;
        lobbyscore.UpdateScoreText1(Maxscore1);
        ////En el caso de que se aumenten diferentes valores de score, se añadiría un parámetro y se usaría la siguiente línea:
        ////score += newScore
        //score += 50;
        //PlayerPrefs.SetInt("GameScore", score);
        //PlayerPrefs.SetInt("TotalScore", score);
    }
    public void UpdateMaxScore2(int points)
    {
        MaxScore2 = points;
        lobbyscore.UpdateScoreText2(MaxScore2);
    }

}
