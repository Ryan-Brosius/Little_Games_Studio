using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimationEvent : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource open;
    [SerializeField] private AudioSource close;

    public void playOpen()
    {
        open.Play();
    }

    public void playClose()
    {
        close.Play();
    }
}
