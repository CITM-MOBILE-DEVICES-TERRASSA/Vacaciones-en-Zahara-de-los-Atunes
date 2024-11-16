using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManagerLevel2 : MonoBehaviour
{
    public int score1 = 0;
    public int maxscore1 = 0;
    public int score2 = 0; // Puntaje inicial
    public int maxscore2 = 0;
    public TextMeshProUGUI scoreText; // Referencia al texto de puntaje en el Canvas

    void Start()
    {
        
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
        }
        else
        {
            Debug.LogError("Score Text no está asignado en el ScoreManager.");
        }
    }
}
