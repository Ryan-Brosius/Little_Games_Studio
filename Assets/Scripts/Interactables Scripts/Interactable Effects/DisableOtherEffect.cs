using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOtherEffect : MonoBehaviour, IInteractionEffect
{
    [SerializeField] GameObject interactableOther;
    public void ExecuteEffect(GameObject gameObject, Interactable interactable)
    {
        Destroy(interactableOther.GetComponent<Interactable>());
    }
}

