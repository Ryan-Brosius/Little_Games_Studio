using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendGameManagerMessageEffect : MonoBehaviour, IInteractionEffect
{
    [Header("Message Properties")]
    [SerializeField] private int message = 0;

    public void ExecuteEffect(GameObject gameObject, Interactable interactable)
    {
        GameManager.Instance.recieveMessage(message);
    }
}
