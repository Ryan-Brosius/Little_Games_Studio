using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ItemDropInteractable : MonoBehaviour, IInteractionEffect
{
    [Header("Furniture Drop Settings")]
    [SerializeField] public GameObject furnitureDrop;
    [SerializeField] public float dropAmount = 1;

    public void ExecuteEffect(GameObject gameObject, Interactable interactable)
    {
        if (gameObject.tag == "Furniture")
        {
            for (int i = 0; i < dropAmount; i++)
            {
                Instantiate(furnitureDrop, gameObject.transform.position, gameObject.transform.rotation);
            }
        }
    }
}
