using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] Timer timer;
    private float totalTime;
    private float remainingTime;

    [SerializeField] private List<Light> sceneLights;

    //bunch of bools to keep track of state
    private bool sentOutFirstMessage = false;

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
    }

    public void recieveMessage(int message)
    {
        //Message 0 -> Used Alarm System
        if (message == 0) StartCoroutine(WaitToTurnOffLights());
    }

    IEnumerator WaitToTurnOffLights()
    {
        yield return new WaitForSeconds(5);
        turnOffAllLights();
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
}
