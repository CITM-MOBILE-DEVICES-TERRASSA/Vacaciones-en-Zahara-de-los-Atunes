using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timeLeft = 150f;
    public TextMeshProUGUI countdownText;

    // Update is called once per frame
    void Update()
    {
      if  (timeLeft > 0)
      {
        timeLeft -= Time.deltaTime;
        UpdateTimerDisplay();
      }
      else
      {
        timeLeft = 0;
        TimerEnded();
      }
    }

    void UpdateTimerDisplay()
    {
      int minutes = Mathf.FloorToInt(timeLeft / 60f);
      int seconds = Mathf.FloorToInt(timeLeft % 60f);
      countdownText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    void TimerEnded()
    {
      countdownText.text = "00:00";
      Debug.Log("Time has run out!");
    }
}
