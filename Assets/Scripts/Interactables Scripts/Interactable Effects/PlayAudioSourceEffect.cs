using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioSourceEffect : MonoBehaviour, IInteractionEffect
{
    public void ExecuteEffect(GameObject gameObject, Interactable interactable)
    {
        AudioSource audioSource = GetComponent<AudioSource>();

        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
