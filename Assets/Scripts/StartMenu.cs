using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private GameObject popupBox;
    [SerializeField] private GameObject messageBox;
    [SerializeField] private GameObject message1;
    [SerializeField] private GameObject message2;
    [SerializeField] private GameObject message3;
    [SerializeField] private GameObject message4;
    private GameScript gameScript;

    private void Start()
    {
        gameScript = FindObjectOfType<GameScript>();
        StartCoroutine(startPopup());
    }

    IEnumerator startPopup()
    {
        yield return new WaitForSecondsRealtime(2f);
        popupBox.SetActive(true);
        AudioManager.Instance.Play("ComputerDing");
    }

    public void clickMessagesApp()
    {
        if (!messageBox.activeInHierarchy)
        {
            StartCoroutine(messagesSpawn());
        }
    }

    IEnumerator messagesSpawn()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        messageBox.SetActive(true);
        message1.SetActive(true);
        AudioManager.Instance.Play("ComputerMessage");

        yield return new WaitForSecondsRealtime(3f);
        message2.SetActive(true);
        AudioManager.Instance.Play("ComputerMessage");

        yield return new WaitForSecondsRealtime(4f);
        message3.SetActive(true);
        AudioManager.Instance.Play("ComputerMessage");

        yield return new WaitForSecondsRealtime(3f);
        message4.SetActive(true);
        AudioManager.Instance.Play("ComputerMessage");

        yield return new WaitForSecondsRealtime(5f);
        gameScript.startGame();
    }
}
