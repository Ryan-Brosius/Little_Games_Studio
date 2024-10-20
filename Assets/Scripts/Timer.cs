using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;

    // Update is called once per frame
    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;

            remainingTime -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        }
        //I don't know why it doesn't work I wonted to make the text turn red at last 5 seconds of the timer. 
        else if(remainingTime > 0 && remainingTime < 6)
        {
            timerText.color = Color.red;
        }
        
        else
        {
            remainingTime = 0;
            timerText.color = Color.red;
            timerText.text = "00:00";
        }
        
        
    }
}
