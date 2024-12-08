using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // สำหรับ UI

public class Shopbuy : MonoBehaviour
{
    public string shopName = "Default Shop"; // ชื่อร้าน
    public int shopPrice; // ราคาของร้าน
    public bool isOwned = false; // ผู้เล่นเป็นเจ้าของร้านหรือไม่

    public List<Item> itemsForSale = new List<Item>(); // รายการสินค้าที่มีขายในร้าน
    private PlayerStats playerStats; // อ้างอิงถึง PlayerStats component

    [Header("UI References")]
    public GameObject shopPanel; // อ้างอิงถึง UI Panel ขายของ
    public GameObject orderPanelManagerObject; // GameObject ที่มี OrderPanelManager component
    private OrderPanelManager orderPanelManager; // อ้างอิงถึง OrderPanelManager component

    private void Awake()
    {
        playerStats = FindObjectOfType<PlayerStats>(); // หา PlayerStats component ในฉาก
        if (playerStats == null)
        {
            Debug.LogError("PlayerStats component not found!");
        }

        if (orderPanelManagerObject != null)
        {
            orderPanelManager = orderPanelManagerObject.GetComponent<OrderPanelManager>();
            if (orderPanelManager == null)
            {
                Debug.LogError("OrderPanelManager component not found on assigned GameObject!");
            }
        }
        else
        {
            Debug.LogError("OrderPanelManager GameObject is not assigned!");
        }
    }

    // ฟังก์ชันสำหรับซื้อร้าน
    public bool TryPurchaseShop(float playerMoney, out float remainingMoney)
    {
        if (!isOwned && playerMoney >= shopPrice)
        {
            remainingMoney = playerMoney - shopPrice;
            isOwned = true;
            Debug.Log($"Shop {shopName} purchased! Remaining money: {remainingMoney}");
            return true;
        }
        else
        {
            remainingMoney = playerMoney;
            if (isOwned)
            {
                Debug.Log("This shop is already owned!");
            }
            else
            {
                Debug.Log("Not enough money to purchase this shop!");
            }
            return false;
        }
    }


    // ฟังก์ชันเพิ่มสินค้าในร้าน
    public void AddItemToShop(string itemName, int itemPrice, GameObject itemObject, Sprite itemImage)
    {
        if (isOwned)
        {
            Item newItem = new Item(itemName, itemPrice, itemObject, itemImage);
            itemsForSale.Add(newItem);
            Debug.Log($"Item {itemName} added to {shopName} at price {itemPrice}!");

            // เพิ่มสินค้าไปยัง Order Panel
            if (orderPanelManager != null)
            {
                orderPanelManager.DisplayItemInOrderPanel(itemObject);
            }
        }
        else
        {
            Debug.Log("You need to own this shop to add items.");
        }
    }

    // ฟังก์ชันขายสินค้าให้ NPC
    public void SellItemToNPC(string itemName)
    {
        Item itemToSell = itemsForSale.Find(item => item.itemName == itemName);
        if (itemToSell != null)
        {
            playerStats.AddMoney(itemToSell.itemPrice); // เพิ่มเงินให้ผู้เล่น
            Debug.Log($"Sold {itemToSell.itemName} for {itemToSell.itemPrice}! Total money: {playerStats.TotalMoney}");
        }
        else
        {
            Debug.Log($"Item {itemName} is not available in the shop.");
        }
    }

    // แสดง Panel ขายของ
    private void ShowShopPanel()
    {
        if (shopPanel != null)
        {
            shopPanel.SetActive(true); // แสดง GameObject
        }
        else
        {
            Debug.LogWarning("ShopPanel is not assigned!");
        }
    }

    // ซ่อน Panel ขายของ
    private void HideShopPanel()
    {
        if (shopPanel != null)
        {
            shopPanel.SetActive(false); // ซ่อน GameObject
        }
        else
        {
            Debug.LogWarning("ShopPanel is not assigned!");
        }
    }

    // ฟังก์ชันสำหรับการดึงรายการสินค้าในร้าน
    public List<Item> GetItemsForSale()
    {
        return itemsForSale;
    }

    // ฟังก์ชันสุ่มเลือกสินค้า
    public Item RandomlySelectItem()
    {
        if (itemsForSale.Count > 0)
        {
            int randomIndex = Random.Range(0, itemsForSale.Count);
            return itemsForSale[randomIndex]; // กลับรายการสินค้าโดยตรง
        }
        return null; // หากไม่มีสินค้าในร้าน
    }

    // ฟังก์ชันเพื่อตรวจสอบสินค้าในร้าน
    public Item GetSelectedItem()
    {
        // สุ่มเลือกสินค้าและตรวจสอบ
        return RandomlySelectItem();
    }
}

// คลาสสำหรับสินค้า
[System.Serializable]
public class Item
{
    public string itemName; // ชื่อสินค้า
    public int itemPrice; // ราคาสินค้า
    public GameObject itemObject; // GameObject ของสินค้า
    public Sprite itemImage; // รูปภาพของสินค้า

    public Item(string name, int price, GameObject obj, Sprite image)
    {
        itemName = name;
        itemPrice = price;
        itemObject = obj;
        itemImage = image;
    }
}
