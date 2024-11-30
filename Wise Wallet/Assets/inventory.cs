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
        public int Quantity; // จำนวนสินค้าใน Inventory

        public InventoryItem(string name, float price, Sprite icon, int quantity = 1)
        {
            Name = name;
            Price = price;
            Icon = icon;
            Quantity = quantity;
        }
    }

    [Header("Inventory Settings")]
    public GameObject itemPrefab; // Prefab สำหรับแสดงสินค้าใน Inventory
    public Transform displayContainer; // จุดแสดงผลของสินค้าใน Inventory

    // ฟังก์ชั่นสำหรับเพิ่มสินค้าใน Inventory
    public void AddItemFromShop(Shop.ShopItem shopItem)
    {
        if (shopItem == null)
        {
            Debug.LogError("ShopItem is null! Cannot add to inventory.");
            return;
        }

        // สร้าง InventoryItem ใหม่
        InventoryItem newItem = new InventoryItem(shopItem.Name, shopItem.Price, shopItem.ItemIcon);
    }

    // สร้างการแสดงผลของสินค้าใน Inventory
    public void CreateItemDisplay(InventoryItem item)
    {
        if (itemPrefab == null)
        {
            Debug.LogError("Item Prefab is not assigned in the Inventory!");
            return;
        }

        if (displayContainer == null)
        {
            Debug.LogError("Display Container is not assigned in the Inventory!");
            return;
        }

        // สร้าง Prefab ใหม่
        GameObject newItem = Instantiate(itemPrefab, displayContainer);

        // การตั้งค่าข้อมูลของสินค้าใน Prefab
        var itemDisplay = newItem.GetComponent<InventoryItemDisplay>();
        if (itemDisplay != null)
        {
            itemDisplay.SetItem(item.Name, item.Price, item.Icon, null);  // ไม่ใช้ PriceObject
        }

        Debug.Log($"Added item to inventory: {item.Name} (Price: {item.Price})");
    }
}
