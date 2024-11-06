using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public GameOverScreen GameOverScreen;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;

    // Update is called once per frame
    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;

            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            int milliseconds = Mathf.FloorToInt((remainingTime * 100) % 100);
            timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);

            if (remainingTime <= 6)
            {
                timerText.color = Color.red;
            }
        }
        else
        {
            remainingTime = 0;
            timerText.color = Color.red;
            timerText.text = "00:00:00";
            GameOver();

        }
        
        
        void GameOver()
        {
            GameOverScreen.Setup();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
