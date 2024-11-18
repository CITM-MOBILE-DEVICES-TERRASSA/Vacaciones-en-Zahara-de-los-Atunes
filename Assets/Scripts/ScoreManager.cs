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

    private int MaxScore1;
    private int MaxScore2;
    private int MaxTotal;
    public TextMeshProUGUI textScore1;
    public TextMeshProUGUI textScore2;
    private UpdateLobbyScore lobbyscore;
    void Start()
    {
        score = PlayerPrefs.GetInt("GameScore");
        totalScore = PlayerPrefs.GetInt("TotalScore");
        lobbyscore = FindObjectOfType<UpdateLobbyScore>();
    }
    private void Update()
    {
        ShowScoreText1();
        ShowScoreText2();
    }

    public void UpdateMaxScore1(int points)
    {
        MaxScore1 = points;
        UpdateTotalScore(MaxScore1);
    }
    private void ShowScoreText1()
    {
        int score = PlayerPrefs.GetInt("Level1Score", MaxScore1); 
        textScore1.text = score.ToString();
    }
    public void UpdateMaxScore2(int points)
    {
        MaxScore2 = points;
        UpdateTotalScore(MaxScore2);
    }
    private void ShowScoreText2()
    {
        int score = PlayerPrefs.GetInt("Level2Score", MaxScore2); 
        textScore2.text = score.ToString();
    }
    public void UpdateTotalScore(int points){
        int suma = 0;
        suma = points - MaxTotal;
        MaxTotal = MaxTotal + suma;
        lobbyscore.UpdateTotalScoreText	(MaxTotal);

    }

}
