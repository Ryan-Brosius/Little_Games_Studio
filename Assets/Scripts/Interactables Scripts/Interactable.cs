using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool isInteractable = true;
    private List<IInteractionEffect> interactionEffects;
    private List<IConditionalCheck> conditionalChecks;

    private void Start()
    {
        interactionEffects = new List<IInteractionEffect>(GetComponents<IInteractionEffect>());
        conditionalChecks = new List<IConditionalCheck>(GetComponents<IConditionalCheck>());
    }

    public void Interact()
    {
        if (isInteractable)
        {
            bool execute = true;

            foreach (var conditional in conditionalChecks)
            {
                execute &= conditional.CanExecuteEffect(gameObject, this);
            }

            if (execute)
            {
                foreach (var effect in interactionEffects)
                {
                    effect.ExecuteEffect(gameObject, this);
                }
                //isInteractable = false;
            }
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
