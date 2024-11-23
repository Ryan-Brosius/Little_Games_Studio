using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendMessageFailEffect : MonoBehaviour, IInteractionFailEffect
{
    [SerializeField] private string message = "foobar";
    public void ExecuteEffect(GameObject gameObject, Interactable interactable)
    {
        MessageSystem.Instance.queueMessage(message);
    }
}
