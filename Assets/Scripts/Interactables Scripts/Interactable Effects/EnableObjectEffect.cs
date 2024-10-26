using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObjectEffect : MonoBehaviour, IInteractionEffect
{
    [SerializeField] private GameObject sceneObject;

    public void ExecuteEffect(GameObject gameObject, Interactable interactable)
    {
        sceneObject.SetActive(true);
    }
}
