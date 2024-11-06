using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class inventoryItem
{
    [Tooltip("Name of the inventory item")]
    public string itemName;

    [Tooltip("UI GameObject with an Image component to display the item icon")]
    public GameObject slotUI;

    [Tooltip("Default sprite for the inventory slot")]
    public Sprite defaultSprite;

    [Tooltip("Sprite when the item is in inventory")]
    public Sprite selectedSprite;

    [Tooltip("Blackout the non-selected sprite?")]
    public bool blackout;

    public void UpdateSlotSprite(bool isSelected)
    {
        if (slotUI != null)
        {
            Image imageComponent = slotUI.GetComponent<Image>();
            if (imageComponent != null)
            {
                if (isSelected)
                {
                    imageComponent.sprite = selectedSprite;
                    imageComponent.color = Color.white;
                }
                else
                {
                    imageComponent.sprite = defaultSprite;
                    imageComponent.color = Color.black;
                }
            }
        }
    }
}

public class InventoryUI : MonoBehaviour
{
    [Tooltip("List of all UI elements with sprites")]
    [SerializeField] List<inventoryItem> inventoryItems = new List<inventoryItem>();
    InventoryManager inventoryManager;

    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = InventoryManager.Instance;
        inventoryManager.OnInventoryUpdated += updateIcons;
        updateIcons();
    }

    public void updateIcons()
    {
        foreach (var UI in inventoryItems)
        {
            UI.UpdateSlotSprite(inventoryManager.HasItem(UI.itemName));
        }
    }

    private void OnEnable()
    {
        if (InventoryManager.Instance != null)
        {
            inventoryManager.OnInventoryUpdated += updateIcons;
        }
    }

    private void OnDisable()
    {
        if (InventoryManager.Instance != null)
        {
            inventoryManager.OnInventoryUpdated -= updateIcons;
        }
    }
}
