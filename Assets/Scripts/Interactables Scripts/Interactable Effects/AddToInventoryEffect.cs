using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToInventoryEffect : MonoBehaviour, IInteractionEffect
{
    [Header("Inventory Properties")]
    [SerializeField] private string itemName = "foobar";
    [SerializeField] private int quantity = 1;


    public void ExecuteEffect(GameObject gameObject, Interactable interactable)
    {
        InventoryManager.Instance.AddItem(itemName, quantity);
    }
}
