using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] Timer timer;
    public float totalTime { get; private set; }
    public float remainingTime { get; private set; }
    [SerializeField] private Volume postProcessing;
    [SerializeField] private GameObject creature;
    [SerializeField] private Animator creatureAnimator;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerDeadTransform;
    [SerializeField] private Image fadeImage;
    [SerializeField] private GameObject fadeToDarkHud;
    [SerializeField] private GameObject playerHud;
    [SerializeField] private GameObject winHud;

    [Header("Canvas Stuff")]
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private TextMeshProUGUI deathTitle;
    [SerializeField] private TextMeshProUGUI deathArticle;

    //Player needs to pick up the password/open first door
    [Header("Event 0")]
    [SerializeField] private GameObject windowTrigger;

    //First event that takes place, player needs to arm the security system
    [Header("Event 1")]
    [SerializeField] private List<Light> sceneLights;
    [SerializeField] private AudioSource generatorExplode;
    [SerializeField] private GameObject doorJiggleTrigger;
    [SerializeField] private GameObject bushSoundTrigger;
    private bool sentOutFirstMessage = false;
    private bool securityAlarmActivated = false;

    //Second event that takes place, player needs to repair generator
    [Header("Event 2")]
    [SerializeField] private GameObject downStairsTrigger;
    [SerializeField] private AudioSource doorSlamSource;
    [SerializeField] private GameObject oldDoor;
    [SerializeField] private GameObject newDoor;
    [SerializeField] private GameObject newLeftDoor;
    [SerializeField] private GameObject newRightDoor;
    private bool generatorRepaired = false;

    private int windowsRepaired = 0;
    private int ventsRepaired = 0;
    private bool fridgeBlockingDoor = false;
    private bool killedPlayer = false;
    private bool lockedFrontDoor = false;

    /**********************
     * WAYS THAT THE PLAYER DIES!!!
     **********************/
    // Turn on security system
    // Turn on generator
    // Board up windows
    // Close vents
    // Block back door


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        totalTime = timer.remainingTime;
    }

    private void Update()
    {
        remainingTime = timer.remainingTime;
        
        if (!sentOutFirstMessage && (totalTime - remainingTime) > 1f)
        {
            MessageSystem.Instance.queueMessage("I think I should arm the security system...");
            sentOutFirstMessage = true;
        }

        UpdateGrain();

        if (remainingTime < 0f && !killedPlayer)
        {
            StartCoroutine(KillPlayer());
            killedPlayer = true;
        }
    }

    private void UpdateGrain()
    {
        postProcessing.profile.TryGet(out FilmGrain f);
        f.intensity.value = Mathf.Lerp(0.5f, 0.1f, remainingTime / totalTime);
    }

    public void recieveMessage(int message)
    {
        //Message 0 -> Used Alarm System
        if (message == 0) StartCoroutine(WaitToTurnOffLights());

        //Message 1 -> Activated Generator
        if (message == 1) StartCoroutine(SlamBasementDoor());

        //Message 2 -> Collected Stickynote
        if (message == 2) windowTrigger.SetActive(true);

        //Message 3 -> Ran into window trigger
        if (message == 3) StartCoroutine(WindowScare());

        //Message 4 -> Boarded window
        if (message == 4) windowsRepaired++;

        //Message 5 -> Fixed vent
        if (message == 5) ventsRepaired++;

        //Message 6 -> Fridge Moved
        if (message == 6) fridgeBlockingDoor = !fridgeBlockingDoor;

        //Message 7 -> Lock Front Door
        if (message == 7) lockedFrontDoor = true;

        if (PlayerWin())
        {
            StartCoroutine(SetPlayerWin());
        }
    }

    public bool getMessage(int message)
    {
        if (message == 0) return securityAlarmActivated;

        return false;
    }

    private IEnumerator WindowScare()
    {
        creatureAnimator.SetTrigger("RunWindow");
        yield return new WaitForSeconds(.3f);
        AudioManager.Instance.Play("HorrorSound");
    }

    private IEnumerator WaitToTurnOffLights()
    {
        securityAlarmActivated = true;
        yield return new WaitForSeconds(2.5f);
        generatorExplode.Play();
        yield return new WaitForSeconds(0.2f);
        turnOffAllLights();
        MessageSystem.Instance.queueMessage("Looks like my power just went out... I can turn the power back on by turning on the generator outside");
        doorJiggleTrigger.SetActive(true);
        //downStairsTrigger.SetActive(true);
        bushSoundTrigger.SetActive(true);
    }

    private void turnOffAllLights()
    {
        foreach (Light light in sceneLights)
        {
            if (light != null)
            {
                light.intensity = 0.0f;
            }
        }
    }

    IEnumerator SlamBasementDoor()
    {
        generatorRepaired = true;
        creature.SetActive(true);
        yield return new WaitForSeconds(1f);
        oldDoor.SetActive(false);
        newDoor.SetActive(true);
        newLeftDoor.GetComponent<Interactable>().enabled = false;
        newRightDoor.GetComponent<Interactable>().enabled = false;
        doorSlamSource.Play();
        yield return new WaitForSeconds(1f);
        creatureAnimator.SetTrigger("MetalDoor");
        yield return new WaitForSeconds(8.6f);
        newLeftDoor.GetComponent<Interactable>().enabled = true;
        newRightDoor.GetComponent<Interactable>().enabled = true;
        creature.SetActive(false);
    }

    IEnumerator KillPlayer()
    {
        creature.SetActive(true);
        StartCoroutine(FadeToDark(1.0f));
        yield return new WaitForSeconds(1.2f);
        playerHud.SetActive(false);
        player.GetComponent<PlayerController>().canBreath = false;
        player.GetComponent<PlayerController>().canMove = false;
        player.transform.position = playerDeadTransform.position;
        player.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
        Camera.main.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
        StartCoroutine(FadeToClear(1f));
        yield return new WaitForSeconds(1f);

        creatureAnimator.SetTrigger("KillPlayer");
        yield return new WaitForSeconds(3f);

        float elapsedTime = 0.0f;
        float timeToRotatePlayer = 0.45f;
        Quaternion startRotation = player.transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0.0f, 180.0f, 0.0f);

        while (elapsedTime < timeToRotatePlayer)
        {
            player.transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / timeToRotatePlayer);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        player.transform.rotation = endRotation;

        yield return new WaitForSeconds(3.5f);
        AudioManager.Instance.Play("GameOverSound");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        deathScreen.SetActive(true);
        deathArticle.text = WhyPlayerDied();
    }

    IEnumerator SetPlayerWin()
    {
        StartCoroutine(FadeToDark(1.0f));
        yield return new WaitForSeconds(1.2f);
        playerHud.SetActive(false);
        timer.remainingTime = 9999;
        AudioManager.Instance.Play("GameWinSound");
        player.GetComponent<PlayerController>().canBreath = false;
        player.GetComponent<PlayerController>().canMove = false;
        StartCoroutine(FadeToClear(1f));
        winHud.SetActive(true);
        yield return new WaitForSeconds(1f);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private string WhyPlayerDied()
    {
        // Turn on security system
        // Turn on generator
        // Lock front door
        // Board up windows
        // Close vents
        // Block back door

        if (!securityAlarmActivated)
        {
            return "Tragedy Strikes in Home Invasion After Alarm Left Off: " +
                   "A local resident lost their life during a home invasion late " +
                   "last night when their security alarm was found deactivated, leaving " +
                   "them vulnerable to the intruder's attack, authorities report.";
        }

        if (!generatorRepaired)
        {
            return "Tragedy Strikes in Home Invasion After Power Cut Off: " +
                   "A local resident lost their life during a home invasion late " +
                   "last night when their power was found to be tampered with, leaving " +
                   "them vulnerable to the intruder's attack, authorities report.";
        }

        if (!lockedFrontDoor)
        {
            return "Tragedy Strikes in Home Invasion After Front Door Unlocked: " +
                   "A local resident lost their life during a home invasion late " +
                   "last night when their front door was unlocked, leaving " +
                   "them vulnerable to the intruder's attack, authorities report.";
        }

        if (windowsRepaired < 7)
        {
            return "Tragedy Strikes in Home Invasion After Window Broken Into: " +
                   "A local resident lost their life during a home invasion late " +
                   "last night where their windows were found broken, leaving " +
                   "them vulnerable to the intruder's attack, authorities report.";
        }

        if (ventsRepaired < 3)
        {
            return "Tragedy Strikes in Home Invasion After Vents Into: " +
                   "A local resident lost their life during a home invasion late " +
                   "last night where their vents were found broken, leaving " +
                   "them vulnerable to the intruder's attack, authorities report.";
        }

        if (!fridgeBlockingDoor)
        {
            return "Tragedy Strikes in Home Invasion Back Door Broken Into: " +
                   "A local resident lost their life during a home invasion late " +
                   "last night where their back door was found broken, leaving " +
                   "them vulnerable to the intruder's attack, authorities report.";
        }

        return "This is an error message and you should not be seeing this lol";
    }

    private bool PlayerWin()
    {
        return securityAlarmActivated &&
               generatorRepaired &&
               lockedFrontDoor &&
               windowsRepaired >= 7 &&
               ventsRepaired >= 3 &&
               fridgeBlockingDoor;
    }

    public IEnumerator FadeToDark(float fadeDuration)
    {
        fadeToDarkHud.SetActive(true);
        float elapsedTime = 0.0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = 1.0f;
        fadeImage.color = color;
    }

    public IEnumerator FadeToClear(float fadeDuration)
    {
        float elapsedTime = 0.0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = 1.0f - Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = 0.0f;
        fadeImage.color = color;
        fadeToDarkHud.SetActive(false);
    }
}
