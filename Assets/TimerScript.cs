using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerScript : MonoBehaviour
{
    public float totalTime = 30f; // The total time in seconds
    private float currentTime; // The current time
   public TextMeshProUGUI timerText; // The text object to display the timer
   public MenuHandler menuHandler;// The menu handler script, which is used to show the game over menu when the timer reaches zero
   private void Start()
    {
         // Set the current time to the total time
        currentTime = totalTime;
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;

         // If the current time is less than or equal to zero, then the game is over
        if (currentTime <= 0f)
        {
            currentTime = totalTime;
            menuHandler.GameOver();
        }

        // Calculate minutes and seconds
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        // Update the text value to display the current time in mm:ss format
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
