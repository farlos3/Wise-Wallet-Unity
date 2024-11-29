using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Mission : MonoBehaviour
{
    [SerializeField]
    private List<ShopRequirement> shopRequirements; // รายการร้านค้าและสินค้าที่ต้องซื้อในแต่ละร้าน

    [SerializeField]
    private float minimumRemainingMoney; // เงินขั้นต่ำที่ต้องเหลือ

    // ฟังก์ชันคืนค่ารายการสินค้าที่ต้องการในรูปแบบ Dictionary
    public Dictionary<Shop, List<GameObject>> GetShopRequirements()
    {
        return shopRequirements.ToDictionary(req => req.Shop, req => req.RequiredItems);
    }

    public float GetMinimumRemainingMoney()
    {
        return minimumRemainingMoney;
    }

    // ฟังก์ชันตรวจสอบว่า Mission สำเร็จหรือไม่
    public bool CheckMissionCompletion(Player player)
    {
        // ตรวจสอบสินค้าในแต่ละร้าน
        foreach (var shopRequirement in shopRequirements)
        {
            Shop shop = shopRequirement.Shop;
            List<GameObject> requiredItems = shopRequirement.RequiredItems;

            foreach (GameObject item in requiredItems)
            {
                string itemName = item.name;

                // ตรวจสอบว่าผู้เล่นมีสินค้าครบหรือไม่
                int itemCount = player.Inventory.Count(i => i == itemName);
                if (itemCount < 1) // ต้องมีอย่างน้อย 1 ชิ้น
                {
                    Debug.Log($"ผู้เล่นยังไม่ได้ซื้อ {itemName} จากร้าน {shop.ShopName}");
                    return false; // เงื่อนไขยังไม่ผ่าน
                }
            }
        }

        // ตรวจสอบว่าเงินเหลือมากกว่าหรือเท่ากับที่กำหนด
        if (player.TotalMoney < minimumRemainingMoney)
        {
            Debug.Log($"ผู้เล่นมีเงินเหลือไม่ถึง {minimumRemainingMoney} (ปัจจุบัน: {player.TotalMoney})");
            return false;
        }

        return true; // Mission สำเร็จ
    }

    // ฟังก์ชันแสดงรายละเอียดของ Mission
    public void DisplayMissionDetails()
    {
        Debug.Log("Mission Objectives:");
        foreach (var shopRequirement in shopRequirements)
        {
            Debug.Log($"ร้าน: {shopRequirement.Shop.ShopName}");
            foreach (var item in shopRequirement.RequiredItems)
            {
                Debug.Log($"- {item.name}");
            }
        }
        Debug.Log($"Minimum Money to Keep: {minimumRemainingMoney}");
    }
}

// คลาสสำหรับกำหนดเงื่อนไขของแต่ละร้าน
[System.Serializable]
public class ShopRequirement
{
    public Shop Shop; // ร้านค้า
    public List<GameObject> RequiredItems; // รายการสินค้าที่ต้องซื้อในร้านนั้น

    public ShopRequirement(Shop shop, List<GameObject> requiredItems)
    {
        Shop = shop;
        RequiredItems = requiredItems;
    }
}

// ตัวอย่าง Player ปรับปรุง
public class Player : MonoBehaviour
{
    public float TotalMoney { get; private set; }
    public List<string> Inventory { get; private set; }

    public Player(float totalMoney)
    {
        TotalMoney = totalMoney;
        Inventory = new List<string>();
    }

    public void AddItem(string itemName)
    {
        Inventory.Add(itemName);
    }

    public void AddMoney(float amount)
    {
        TotalMoney += amount;
    }

    public void SpendMoney(float amount)
    {
        if (TotalMoney >= amount)
        {
            TotalMoney -= amount;
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }

    public bool HasItem(string itemName, int quantity)
    {
        return Inventory.Count(i => i == itemName) >= quantity;
    }
}
