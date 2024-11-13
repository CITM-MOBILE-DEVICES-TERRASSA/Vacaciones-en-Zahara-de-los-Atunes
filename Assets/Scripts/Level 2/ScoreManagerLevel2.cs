using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManagerLevel2 : MonoBehaviour
{
    public int score = 0; // Puntaje inicial
    public TextMeshProUGUI scoreText; // Referencia al texto de puntaje en el Canvas

    void Start()
    {
        UpdateScoreText();
    }

    // Método para actualizar el puntaje
    public void UpdateScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    // Método para actualizar el texto del puntaje en pantalla
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Puntos: " + score;
        }
        else
        {
            Debug.LogError("Score Text no está asignado en el ScoreManager.");
        }
    }
}
