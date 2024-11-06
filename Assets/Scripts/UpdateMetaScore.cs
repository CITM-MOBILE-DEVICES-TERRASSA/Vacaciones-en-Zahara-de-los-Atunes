using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UpdateMetaScore : MonoBehaviour
{
    public TextMeshProUGUI metaScoreText;

    private void Update()
    {
        UpdateMetaScoreText();
    }

    private void UpdateMetaScoreText()
    {
        metaScoreText.text = PlayerPrefs.GetInt("GameScore").ToString();
    }
}
