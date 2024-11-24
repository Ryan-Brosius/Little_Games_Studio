using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioSourceEffect : MonoBehaviour, IInteractionEffect
{
    [SerializeField] AudioSource audioSource;
    public void ExecuteEffect(GameObject gameObject, Interactable interactable)
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
