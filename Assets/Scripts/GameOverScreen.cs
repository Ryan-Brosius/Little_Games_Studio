using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public TMP_Text EnterPoint;
    
    public void Setup()
    {
        gameObject.SetActive(true);

    }
    
    public void RestartButton()
    {
        SceneManager.LoadScene("SampleScene");
    }
    
    public void ExitButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
