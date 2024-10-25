using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool isInteractable = true;
    private List<IInteractionEffect> interactionEffects;

    private void Start()
    {
        interactionEffects = new List<IInteractionEffect>(GetComponents<IInteractionEffect>());
    }

    public void Interact()
    {
        if (isInteractable)
        {
            foreach (var effect in interactionEffects)
            {
                effect.ExecuteEffect(gameObject, this);
            }

            isInteractable = false;
        }
    }

    public void OnHoverEnter()
    {
        if (isInteractable)
        {
            //do things here if needed in future
        }
    }

    public void OnHoverExit()
    {
        //do other things here if needed in future
    }
}
