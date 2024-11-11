using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour, IInteractionEffect
{
    public GameObject lightSource;
    public bool isOn = true;

    public void ExecuteEffect(GameObject gameObject, Interactable interactable)
    {
        if (isOn)
        {
            lightSource.SetActive(false);
            isOn = false;
        }

        else if (!isOn)
        {
            lightSource.SetActive(true);
            isOn = true;
        }
    }
}
