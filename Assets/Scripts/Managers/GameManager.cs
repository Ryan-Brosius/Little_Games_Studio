using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] Timer timer;
    public float totalTime { get; private set; }
    public float remainingTime { get; private set; }
    [SerializeField] private Volume postProcessing;


    //First event that takes place, player needs to arm the security system
    [Header("Event 1")]
    [SerializeField] private List<Light> sceneLights;
    [SerializeField] private AudioSource generatorExplode;
    [SerializeField] private GameObject doorJiggleTrigger;
    private bool sentOutFirstMessage = false;

    //Second event that takes place, player needs to repair generator
    [Header("Event 2")]
    [SerializeField] private GameObject downStairsTrigger;
    [SerializeField] private AudioSource doorSlamSource;
    [SerializeField] private GameObject oldDoor;
    [SerializeField] private GameObject newDoor;
    private bool generatorRepaired = false;


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
        
        if (!sentOutFirstMessage && (totalTime - remainingTime) > 1.5f)
        {
            MessageSystem.Instance.queueMessage("I think I should arm the security system...");
            sentOutFirstMessage = true;
        }

        UpdateGrain();
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

        if (message == 1) StartCoroutine(SlamBasementDoor());
    }

    IEnumerator WaitToTurnOffLights()
    {
        yield return new WaitForSeconds(5);
        generatorExplode.Play();
        yield return new WaitForSeconds(0.2f);
        turnOffAllLights();
        doorJiggleTrigger.SetActive(true);
        downStairsTrigger.SetActive(true);
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
        yield return new WaitForSeconds(1f);
        oldDoor.SetActive(false);
        newDoor.SetActive(true);
        doorSlamSource.Play();
    }
}
