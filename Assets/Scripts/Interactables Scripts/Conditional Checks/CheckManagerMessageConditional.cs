using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckManagerMessageConditional : MonoBehaviour, IConditionalCheck
{
    [SerializeField] int message;

    public bool CanExecuteEffect(GameObject gameObject, Interactable interactable)
    {
        return GameManager.Instance.getMessage(message);
    }

}
