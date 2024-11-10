using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InventoryItem
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
    [SerializeField] List<InventoryItem> inventoryItems = new List<InventoryItem>();
    InventoryManager inventoryManager;

    [Header("Animation Settings")]
    [SerializeField] private float animationDuration = 0.2f;
    [SerializeField] private float scaleMultiplier = 1.5f;
    [SerializeField] private float rotationAngle = 5f;

    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = InventoryManager.Instance;
        inventoryManager.OnInventoryUpdated += updateIcons;
        setAllIcons();
    }

    public void setAllIcons()
    {
        foreach (var UI in inventoryItems)
        {
            UI.UpdateSlotSprite(inventoryManager.HasItem(UI.itemName));
        }
    }

    public void updateIcons(string item)
    {
        foreach (var UI in inventoryItems)
        {
            if (UI.itemName.ToUpper() == item.ToUpper())
            {
                UI.UpdateSlotSprite(inventoryManager.HasItem(UI.itemName));
                animateInventoryItem(UI);
                break;
            }
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

    private void animateInventoryItem(InventoryItem inventoryItem)
    {
        StartCoroutine(AnimateItemCoroutine(inventoryItem));
    }

    private IEnumerator AnimateItemCoroutine(InventoryItem inventoryItem)
    {
        Transform itemTransform = inventoryItem.slotUI.transform;

        Vector3 originalScale = itemTransform.localScale;
        Quaternion originalRotation = itemTransform.localRotation;

        Vector3 targetScale = originalScale * scaleMultiplier;
        Quaternion targetRotation = originalRotation * Quaternion.Euler(0, 0, rotationAngle);

        float halfDuration = animationDuration / 2f;
        float elapsedTime = 0f;

        while (elapsedTime < halfDuration)
        {
            float t = elapsedTime / halfDuration;
            itemTransform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            itemTransform.localRotation = Quaternion.Lerp(originalRotation, targetRotation, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        itemTransform.localScale = targetScale;
        itemTransform.localRotation = targetRotation;

        elapsedTime = 0f;
        while (elapsedTime < halfDuration)
        {
            float t = elapsedTime / halfDuration;
            itemTransform.localScale = Vector3.Lerp(targetScale, originalScale, t);
            itemTransform.localRotation = Quaternion.Lerp(targetRotation, originalRotation, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        itemTransform.localScale = originalScale;
        itemTransform.localRotation = originalRotation;
    }
}
