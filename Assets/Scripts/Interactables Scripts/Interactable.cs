using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public bool isInteractable = true;
    private List<IInteractionEffect> interactionEffects;
    private List<IConditionalCheck> conditionalChecks;
    private List<IInteractionFailEffect> failEffects;
    [SerializeField] private bool disableOnUse = true;

    private GameObject cursor;

    private void Start()
    {
        interactionEffects = new List<IInteractionEffect>(GetComponents<IInteractionEffect>());
        conditionalChecks = new List<IConditionalCheck>(GetComponents<IConditionalCheck>());
        failEffects = new List<IInteractionFailEffect>(GetComponents<IInteractionFailEffect>());

        cursor = GameObject.FindWithTag("Cursor");
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

                if (disableOnUse)
                {
                    isInteractable = false;
                }
            }
            else
            {
                foreach (var effect in failEffects)
                {
                    effect.ExecuteEffect(gameObject, this);
                }
            }
        }
    }



    public void OnHoverEnter()
    {
        if (isInteractable)
        {
            if (cursor) cursor.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
    }

    public void OnHoverExit()
    {
        if (cursor) cursor.GetComponent<Image>().color = new Color(1, 1, 1, 0);
    }
}
