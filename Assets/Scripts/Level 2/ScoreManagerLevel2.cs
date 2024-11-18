using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManagerLevel : MonoBehaviour
{
    private int score1 = 0;
    private int maxscore1 = 0;
    private int score2 = 0; // Puntaje inicial
    private int maxscore2 = 0;
    public TextMeshProUGUI scoreText; // Referencia al texto de puntaje en el Canvas
    private ScoreManager scoremanager;

    void Start()
    {
         scoremanager = FindObjectOfType<ScoreManager>();
    }
    public void UpdateScoreLevel1(int points)
    {
        score1 += points;
        UpdateScoreText1();
    }

    private void UpdateScoreText1()
    {
        if (scoreText != null)
        {
            scoreText.text = "Puntos: " + score1;
            if(score1 > maxscore1)
            {
                maxscore1 = score1;
                scoremanager.UpdateMaxScore1(maxscore1);
            }
        }
        else
        {
            Debug.LogError("Score Text no está asignado en el ScoreManager.");
        }
    }

// Método para actualizar el puntaje
public void UpdateScoreLevel2(int points)
    {
        score2 += points;
        UpdateScoreText2();
    }

    // Método para actualizar el texto del puntaje en pantalla
    private void UpdateScoreText2()
    {
        if (scoreText != null)
        {
            scoreText.text = "Puntos: " + score2;
            if(score2 > maxscore2)
            {
                maxscore2 = score2;
                scoremanager.UpdateMaxScore2(maxscore2);
            }
        }
        
        else
        {
            Debug.LogError("Score Text no está asignado en el ScoreManager.");
        }
    }
}
