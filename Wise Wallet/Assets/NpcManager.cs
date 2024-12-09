using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcManager : MonoBehaviour
{
    public List<OwnerShop> allShops; // รายการร้านค้าทั้งหมดใน Scene
    public Transform displayArea; // บริเวณที่จะแสดง Prefab
    public GameObject itemPrefab; // Prefab ที่จะใช้ในการแสดงผล (ต้องมี NpcDisplay)

    private void Start()
    {
        // ค้นหา OwnerShop ทั้งหมดใน Scene ถ้าไม่ตั้งค่าใน Inspector
        if (allShops == null || allShops.Count == 0)
        {
            allShops = new List<OwnerShop>(FindObjectsOfType<OwnerShop>());
            if (allShops.Count == 0)
            {
                Debug.LogError("No OwnerShop components found in the scene!");
            }
        }
    }

    // ฟังก์ชันค้นหาร้านค้าและสุ่มไอเท็ม
    public void GetItemsFromShop(string shopName)
    {
        if (itemPrefab == null)
        {
            Debug.LogError("Item Prefab is not assigned!");
            return;
        }

        // ตรวจสอบว่า Prefab มี NpcDisplay อยู่หรือไม่
        NpcDisplay npcDisplay = itemPrefab.GetComponent<NpcDisplay>();
        if (npcDisplay == null)
        {
            Debug.LogError("Prefab does not contain an NpcDisplay component!");
            return;
        }

        // ค้นหาร้านค้าด้วยชื่อ
        OwnerShop targetShop = allShops.Find(shop => shop.ShopName == shopName);

        if (targetShop == null)
        {
            Debug.LogWarning($"Shop with name {shopName} not found!");
            return;
        }

        // แสดงข้อมูลไอเท็มในร้านค้า
        Debug.Log($"Items in shop {shopName}:");
        foreach (ShopItem item in targetShop.ShopItems)
        {
            Debug.Log($"- ItemName: {item.ItemName}, ItemID: {item.ItemID}, ItemPrice: {item.ItemPrice}, ItemImage: {item.ItemImage.name}");
        }

        // สุ่มไอเท็ม 3 ชิ้นโดยไม่ซ้ำกัน
        if (targetShop.ShopItems.Count >= 3)
        {
            List<ShopItem> randomItems = new List<ShopItem>();
            List<int> usedIndices = new List<int>();

            while (randomItems.Count < 3)
            {
                int randomIndex = Random.Range(0, targetShop.ShopItems.Count);
                if (!usedIndices.Contains(randomIndex))
                {
                    usedIndices.Add(randomIndex);
                    randomItems.Add(targetShop.ShopItems[randomIndex]);
                }
            }

            // สร้าง Prefab ในพื้นที่ Display Area
            foreach (ShopItem item in randomItems)
            {
                GameObject newDisplay = Instantiate(itemPrefab, displayArea);
                NpcDisplay display = newDisplay.GetComponent<NpcDisplay>();

                if (display != null)
                {
                    // อัปเดตข้อมูลใน NpcDisplay
                    display.UpdateDisplay(item.ItemID, item.ItemImage);
                }
            }
        }
        else
        {
            Debug.LogWarning($"Shop {shopName} does not have enough items to perform a random selection!");
        }
    }
}
