using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    //Singleton lol
    public static InventoryManager Instance { get; private set; }

    private Dictionary<string, int> items = new Dictionary<string, int>();


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
        if (items.ContainsKey(item))
        {
            items[item] += quantity;
        }
        else
        {
            items[item] = quantity;
        }

    }

    public bool RemoveItem(string item, int quantity = 1)
    {
        if (items.ContainsKey(item))
        {
            if (items[item] >= quantity)
            {
                items[item] -= quantity;

                if (items[item] == 0)
                {
                    items.Remove(item);
                }

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
        return items.ContainsKey(item) && items[item] >= quantity;
    }

    public int GetItemQuantity(string item)
    {
        return items.ContainsKey(item) ? items[item] : 0;
    }
}

