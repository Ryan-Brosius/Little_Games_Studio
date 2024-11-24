using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    [Header("Monitor Main Menu Settings")]
    [SerializeField] public Canvas gameHud;
    [SerializeField] public Canvas monitorMenu;
    [SerializeField] public Camera playerCamera;
    [SerializeField] public Camera monitorCamera;
    [SerializeField] public float transitionTime = 2f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        gameStartingScreen();
    }

    public void gameStartingScreen()
    {
        Time.timeScale = 0;

        gameHud.enabled = false;
        monitorMenu.enabled = true;

        playerCamera.gameObject.SetActive(false);
        monitorCamera.gameObject.SetActive(true);
    }

    public void startGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        StartCoroutine(MoveCameraCoroutine());
    }

    IEnumerator MoveCameraCoroutine()
    {
        float elapsedTime = 0f;
        Vector3 startPosition = monitorCamera.transform.position;
        Quaternion startRotation = monitorCamera.transform.rotation;
        Vector3 endPosition = playerCamera.transform.position;
        Quaternion endRotation = playerCamera.transform.rotation;

        playerCamera.gameObject.SetActive(false);

        while (elapsedTime < transitionTime)
        {
            float t = elapsedTime / transitionTime;
            t = 1f - Mathf.Pow(1f - t, 3f);

            monitorCamera.transform.position = Vector3.Lerp(startPosition, endPosition, t);
            monitorCamera.transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);

            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        monitorCamera.transform.position = endPosition;
        monitorCamera.transform.rotation = endRotation;

        enableGameHud();
    }

    public void enableGameHud()
    {
        Time.timeScale = 1;

        gameHud.enabled = true;
        //monitorMenu.enabled = false;

        playerCamera.gameObject.SetActive(true);
        monitorCamera.gameObject.SetActive(false);
    }
}
