using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    //Singleton lol
    public static InventoryManager Instance { get; private set; }
    private Dictionary<string, int> items = new Dictionary<string, int>();
    public event Action OnInventoryUpdated;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void AddItem(string item, int quantity = 1)
    {
        item = item.ToUpper();
        if (items.ContainsKey(item))
        {
            items[item] += quantity;
        }
        else
        {
            items[item] = quantity;
        }
        NotifyInventoryUpdated();
    }

    public bool RemoveItem(string item, int quantity = 1)
    {
        item = item.ToUpper();
        if (items.ContainsKey(item))
        {
            if (items[item] >= quantity)
            {
                items[item] -= quantity;

                if (items[item] == 0)
                {
                    items.Remove(item);
                }

                NotifyInventoryUpdated();
                return true;
            }
            else
            {
                Debug.LogWarning($"Not enough {item} in inventory to remove.");
                return false;
            }
        }
        else
        {
            Debug.LogWarning($"{item} not found in inventory.");
            return false;
        }
    }

    public bool HasItem(string item, int quantity = 1)
    {
        item = item.ToUpper();
        return items.ContainsKey(item) && items[item] >= quantity;
    }

    public int GetItemQuantity(string item)
    {
        item = item.ToUpper();
        return items.ContainsKey(item) ? items[item] : 0;
    }

    private void NotifyInventoryUpdated()
    {
        OnInventoryUpdated?.Invoke();
    }
}

