using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UpdateLobbyScore : MonoBehaviour
{
    public TextMeshProUGUI lobbyScore;

    private void Update()
    {
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        lobbyScore.text = PlayerPrefs.GetInt("TotalScore").ToString();
    }
}
