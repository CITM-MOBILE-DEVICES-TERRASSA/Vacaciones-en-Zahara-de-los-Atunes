using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game2Manager : MonoBehaviour
{
    public static Game2Manager Instance { get; private set; }

    [Header("Game Settings")]
    public int elephantsToWin = 50;
    public float gameTime = 60f;
    public int maxLives = 3;

    [Header("UI Elements")]
    public Text scoreText;
    public Text timerText;
    public Text livesText;
    public GameObject victoryPanel;
    public GameObject gameOverPanel;
    public Button returnToLobbyButton;
    public Button goToFridgeButton;
    public Button gameOverLobbyButton;  
    public Button restartButton;

    private int score = 0;
    private int elephantsKilled = 0;
    private int currentLives;
    private float currentTime;
    private bool isGameOver = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentLives = maxLives;
        currentTime = gameTime;
        UpdateUI();

        victoryPanel.SetActive(false);
        gameOverPanel.SetActive(false);

        GameStatusManager.Instance.hasWonGame2 = false;
    }

    void Update()
    {
        if (isGameOver) return;

        currentTime -= Time.deltaTime;
        UpdateUI();

        if (currentTime <= 0)
        {
            CheckGameOver();
        }
    }

    public void UpdateScore(int points)
    {
        if (isGameOver) return;

        int newScore = score + points;
        score = Mathf.Max(0, newScore);

        if (points > 0) 
        {
            elephantsKilled++;
            CheckWinCondition();
        }

        UpdateUI();
    }

    public void OnPrincessHit()
    {
        if (isGameOver) return;

        currentLives--;
        UpdateUI();

        if (currentLives <= 0)
        {
            EndGame(false);
        }
    }

    private void CheckWinCondition()
    {
        if (elephantsKilled >= elephantsToWin)
        {
            EndGame(true);
        }
    }

    private void CheckGameOver()
    {
        if (elephantsKilled < elephantsToWin || currentLives <= 0)
        {
            EndGame(false);
        }
    }

    private void EndGame(bool victory)
    {
        isGameOver = true;
        SpawnManager.Instance.StopSpawning();

        if (victory)
        {
            GameStatusManager.Instance.hasWonGame2 = true;
            victoryPanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            GameStatusManager.Instance.hasWonGame2 = false;
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void UpdateUI()
    {
        if (scoreText) scoreText.text = $"Puntos: {score}";
        if (timerText)
        {
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            timerText.text = $"Tiempo: {minutes:00}:{seconds:00}";
        }
        if (livesText) livesText.text = $"Vidas: {currentLives}";
    }

    public void ReturnToLobby()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Lobby"); 
    }

    public void GoToFridge()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Collectibles"); 
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
