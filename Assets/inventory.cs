using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [System.Serializable]
    public class InventoryItem
    {
        public string Name;
        public float Price;
        public Sprite Icon;
        public int Quantity;

        public InventoryItem(string name, float price, Sprite icon, int quantity = 1)
        {
            Name = name;
            Price = price;
            Icon = icon;
            Quantity = quantity;
        }
    }

    [Header("Inventory Settings")]
    public GameObject itemPrefab; // Prefab for displaying inventory items
    public Transform displayContainer; // Container for displaying inventory items

    private Queue<GameObject> itemPool = new Queue<GameObject>();
    private List<InventoryItem> items = new List<InventoryItem>();

    // Add item to inventory from a shop item
    public void AddItemFromShop(Shop.ShopItem shopItem)
    {
        if (shopItem == null)
        {
            Debug.LogError("ShopItem is null! Cannot add to inventory.");
            return;
        }

        InventoryItem existingItem = items.Find(item => item.Name == shopItem.Name);
        if (existingItem != null)
        {
            existingItem.Quantity++;
        }
        else
        {
            InventoryItem newItem = new InventoryItem(shopItem.Name, shopItem.Price, shopItem.ItemIcon);
            items.Add(newItem);
        }

        CreateItemDisplay(shopItem);
    }

    // Remove item from inventory
    public bool RemoveItem(string itemName, int quantity = 1)
    {
        InventoryItem item = items.Find(i => i.Name == itemName);
        if (item != null && item.Quantity >= quantity)
        {
            item.Quantity -= quantity;
            if (item.Quantity <= 0)
            {
                items.Remove(item);
            }
            return true;
        }
        Debug.LogWarning($"Item {itemName} not found or insufficient quantity.");
        return false;
    }

    // Get item from inventory
    public InventoryItem GetItem(string itemName)
    {
        return items.Find(item => item.Name == itemName);
    }

    // Create item display for inventory
    private void CreateItemDisplay(Shop.ShopItem shopItem)
    {
        GameObject displayObject;

        if (itemPool.Count > 0)
        {
            displayObject = itemPool.Dequeue(); // Reuse an item from the pool
            displayObject.SetActive(true);
        }
        else
        {
            displayObject = Instantiate(itemPrefab, displayContainer); // Create a new one if pool is empty
        }

        InventoryItemDisplay itemDisplay = displayObject.GetComponent<InventoryItemDisplay>();
        if (itemDisplay != null)
        {
            itemDisplay.SetItem(shopItem.Name, shopItem.Price, shopItem.ItemIcon);
        }

        Debug.Log($"Added item to inventory display: {shopItem.Name} (Price: {shopItem.Price})");
    }

    // Return item to the pool
    public void ReturnToPool(GameObject item)
    {
        item.SetActive(false);
        itemPool.Enqueue(item);
    }
}