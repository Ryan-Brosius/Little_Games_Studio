using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    [Range(2f, 10f)]
    [SerializeField] private float interactionRange = 5f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private bool debug;
    private bool isTouchingInteractable = false;
    private Interactable currentInteractable; // may need to add in future


    // Update is called once per frame
    void Update()
    {
        CheckForInteractable();
        HandleInteraction();
    }

    void CheckForInteractable()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionRange, interactableLayer))
        {
            isTouchingInteractable = true;
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null && interactable != currentInteractable)
            {
                if (currentInteractable != null)
                {
                    currentInteractable.OnHoverExit();
                }

                currentInteractable = interactable;
                currentInteractable.OnHoverEnter();
            }
        }
        else if (currentInteractable != null)
        {
            isTouchingInteractable = false;
            currentInteractable.OnHoverExit();
            currentInteractable = null;
        }

        if (debug)  // Debug to see what is happening
        {
            debugRay(ray);
        }
    }

    void HandleInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentInteractable != null)
            {
                currentInteractable.Interact();
                currentInteractable = null;
            }
        }
    }

    void debugRay(Ray ray)
    {
        if (isTouchingInteractable)
        {
            Debug.DrawRay(ray.origin, ray.direction * interactionRange, Color.green);
        } else
        {
            Debug.DrawRay(ray.origin, ray.direction * interactionRange, Color.red);
        }
    }
}
