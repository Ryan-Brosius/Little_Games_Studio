using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    [Header("Monitor Main Menu Settings")]
    [SerializeField] public Canvas gameHud;
    [SerializeField] public Canvas monitorMenu;
    [SerializeField] public GameObject playerCamera;
    [SerializeField] public Camera monitorCamera;
    [SerializeField] public float fovSpeed = 0.1f;
    [SerializeField] public float oldFov;
    [SerializeField] public float newFov;
    private float playerFov;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        monitorCamera.fieldOfView = oldFov;

        gameStartingScreen();


    }

    public void gameStartingScreen()
    {
        Time.timeScale = 0;

        gameHud.enabled = false;

        monitorMenu.enabled = true;

        playerCamera.SetActive(false);

        monitorCamera.gameObject.SetActive(true);
    }

    public void startGame()
    {

        Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = false;

        StartCoroutine(changeFov());

    }

    IEnumerator changeFov()
    {
        while (Mathf.Abs(monitorCamera.fieldOfView - newFov) > 0.01f)
        {
            monitorCamera.fieldOfView = Mathf.MoveTowards(monitorCamera.fieldOfView, newFov, fovSpeed);
            yield return null;
        }

        yield return new WaitForSecondsRealtime(1);

        enableGameHud();
    }

    public void enableGameHud()
    {
        Time.timeScale = 1;

        gameHud.enabled = true;

        monitorMenu.enabled = false;

        playerCamera.SetActive(true);

        monitorCamera.gameObject.SetActive(false);
    }
}
