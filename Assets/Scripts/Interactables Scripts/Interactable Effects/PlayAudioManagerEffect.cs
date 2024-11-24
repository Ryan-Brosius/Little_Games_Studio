using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioManagerEffect : MonoBehaviour, IInteractionEffect
{
    [SerializeField] private string audioName = "foobar";
    public void ExecuteEffect(GameObject gameObject, Interactable interactable)
    {
        AudioManager.Instance.Play(audioName);
    }
}
