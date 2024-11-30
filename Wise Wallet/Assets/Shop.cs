using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [System.Serializable]
    public class ShopItem
    {
        public string Name;               // ชื่อของสินค้า
        public float Price;               // ราคา
        public GameObject RelatedObject;  // GameObject ที่เกี่ยวข้องกับสินค้า
        public Sprite ItemIcon;           // ไอคอนของสินค้า

        public ShopItem(string name, float price, GameObject relatedObject = null, Sprite itemIcon = null)
        {
            Name = name;
            Price = price;
            RelatedObject = relatedObject;
            ItemIcon = itemIcon;
        }
    }

    [Header("Shop Settings")]
    public string ShopName;                   // ชื่อร้าน
    [SerializeField] private List<ShopItem> items = new List<ShopItem>(); // รายการสินค้าที่ขายในร้าน

    public Inventory inventory;  // Reference to Inventory
    [SerializeField] public float playerMoney = 1000f;  // ตัวแปรเงินของผู้เล่น

    // Get ShopItem โดยใช้ชื่อสินค้า
    public ShopItem GetItem(string itemName)
    {
        return items.Find(item => item.Name == itemName);
    }

    // ฟังก์ชั่นสำหรับการซื้อสินค้า
    public void BuyItem(string itemName)
    {
        // ตรวจสอบว่า Inventory ถูกตั้งค่าแล้ว
        if (inventory == null)
        {
            Debug.LogError("Inventory reference is not assigned in the Shop!");
            return;
        }

        ShopItem item = GetItem(itemName);  // ดึงข้อมูลสินค้า

        if (item != null && playerMoney >= item.Price)
        {
            playerMoney -= item.Price;  // ลดเงินของผู้เล่น
            inventory.AddItemFromShop(item); // เพิ่มสินค้าใน Inventory

            // สร้าง Prefab ของ Item ใน Inventory Panel
            GameObject newItem = Instantiate(inventory.itemPrefab, inventory.displayContainer);
            InventoryItemDisplay itemDisplay = newItem.GetComponent<InventoryItemDisplay>();

            if (itemDisplay != null)
            {
                // ตั้งค่าข้อมูลสินค้าใน Inventory
                // ใช้ ToString("0.00") เพื่อให้แสดงเป็นตัวเลขโดยไม่ต้องการสัญลักษณ์เงิน
                itemDisplay.SetItem(item.Name, float.Parse(item.Price.ToString("F0")), item.ItemIcon, null);  // ไม่ใช้ PriceObject
            }

            Debug.Log($"Player bought item: {itemName}. Remaining money: {playerMoney}");
        }
        else
        {
            Debug.LogWarning("Not enough money or item not found!");
        }
    }

    // เพิ่มรายการสินค้าในร้าน
    public void AddItem(string name, float price, GameObject relatedObject, Sprite itemIcon)
    {
        ShopItem item = new ShopItem(name, price, relatedObject, itemIcon);
        items.Add(item);
    }
}
