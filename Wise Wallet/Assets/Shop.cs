using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [System.Serializable]
    public class ShopItem
    {
        public string Name;           // ชื่อสินค้า
        public float Price;           // ราคาสินค้า
        public int Quantity = 10;     // จำนวนสินค้า
        public GameObject RelatedObject; // GameObject ที่เกี่ยวข้องกับสินค้า (เช่น ปุ่มใน UI)

        public ShopItem(string name, float price, int quantity = 10, GameObject relatedObject = null)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
            RelatedObject = relatedObject;
        }
    }

    [Header("Shop Settings")]
    public string ShopName;             // ชื่อร้านค้า
    [SerializeField] private List<ShopItem> items; // รายการสินค้าของร้าน

    // Display all items in the shop (for debugging)
    public void DisplayItems()
    {
        Debug.Log($"Items in {ShopName}:");
        foreach (var item in items)
        {
            Debug.Log($"{item.Name} - {item.Price} - Quantity: {item.Quantity}");
        }
    }

    // Get a ShopItem by name
    public ShopItem GetItem(string itemName)
    {
        return items.Find(item => item.Name == itemName);
    }

    // Remove items from the shop's inventory
    public void RemoveItem(string itemName, int quantity)
    {
        ShopItem item = GetItem(itemName);
        if (item != null && item.Quantity >= quantity)
        {
            item.Quantity -= quantity;
            if (item.Quantity <= 0)
                items.Remove(item);
        }
    }

    // Add item to the shop
    public void AddItem(string itemName, float price, int quantity = 10, GameObject relatedObject = null)
    {
        ShopItem item = GetItem(itemName);
        if (item != null)
        {
            item.Quantity += quantity; // Add more if the item already exists
        }
        else
        {
            items.Add(new ShopItem(itemName, price, quantity, relatedObject)); // Add new item
        }
    }

    // Check if player can purchase an item (based on their money)
    public bool CanPlayerBuyItem(string itemName, float playerMoney)
    {
        ShopItem item = GetItem(itemName);
        return item != null && playerMoney >= item.Price;
    }

    // Buy item from the shop
    public bool BuyItem(string itemName, int quantity, ref float playerMoney)
    {
        ShopItem item = GetItem(itemName);
        if (item != null && item.Quantity >= quantity && playerMoney >= item.Price * quantity)
        {
            playerMoney -= item.Price * quantity;
            RemoveItem(itemName, quantity);
            return true;
        }
        return false;
    }

    // Return all items (used for linking with Mission.cs)
    public List<ShopItem> GetAllItems()
    {
        return items;
    }
}
