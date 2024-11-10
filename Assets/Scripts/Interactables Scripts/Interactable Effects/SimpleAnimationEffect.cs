using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimationEffect : MonoBehaviour, IInteractionEffect
{
    [Header("Animation Properties")]
    [SerializeField] private string triggerName = "foobar";
    [SerializeField]  private Animator animator;

    public void ExecuteEffect(GameObject gameObject, Interactable interactable)
    {
        animator.SetTrigger(triggerName);
    }
}
