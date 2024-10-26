using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInventoryConditional : MonoBehaviour, IConditionalCheck
{
    [Header("Inventory Properties")]
    [SerializeField] private string itemName = "foobar";
    [SerializeField] private int quantity = 1;

    public bool CanExecuteEffect(GameObject gameObject, Interactable interactable)
    {
        return InventoryManager.Instance.HasItem(itemName, quantity);
    }
}
