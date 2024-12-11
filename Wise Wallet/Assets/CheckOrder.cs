using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOrder : MonoBehaviour
{
    private OrderManager orderManager;  // ตัวแปรอ้างอิงไปยัง OrderManager
    private NpcManager npcManager; // เพิ่มการอ้างอิงถึง NpcManager
    private List<string> selectedItems = new List<string>();
    private List<ShopItem> npcItems = new List<ShopItem>();
    public GameObject prefabIncorrect; // เพิ่มตัวแปรนี้ในฟิลด์ของคลาส

    private void Start()
    {
        // หา OrderManager จากใน scene หรือจะกำหนดด้วยตัวเองใน Inspector
        orderManager = FindObjectOfType<OrderManager>();
        if (orderManager == null)
        {
            Debug.LogError("ไม่พบ OrderManager ใน scene.");
        }
        npcManager = FindObjectOfType<NpcManager>();
    }

    // ฟังก์ชันที่ดึงข้อมูลที่เลือกจาก OrderManager
    public void GetSelectItem()
    {
        // ดึงเฉพาะ ItemID ที่เลือกไว้จาก OrderManager
        List<string> selectedItemIDs = orderManager.GetSelectedItemIDs();

        // แสดงผลลัพธ์ใน Console
        Debug.Log("Selected Item IDs: ");
        foreach (var itemID in selectedItemIDs)
        {
            Debug.Log(itemID);  // แสดง ItemID ที่เลือก
        }
    }



    // public void GetItemFromNpc()
    // {
    //     // เรียกใช้ GetItemToRandom จาก NpcManager เพื่อสุ่มสินค้าจากร้านค้า
    //     NpcManager npcManager = FindObjectOfType<NpcManager>();  // หาคลาส NpcManager ในฉาก
    //     if (npcManager != null)
    //     {
    //         npcItems = npcManager.GetItemToRandom();  // เก็บผลลัพธ์ที่ได้จาก GetItemToRandom
    //         Debug.Log("Items received from NPC:");
    //         foreach (ShopItem item in npcItems)
    //         {
    //             Debug.Log($"Item: {item.ItemName}");  // แสดงชื่อสินค้าที่ได้รับจาก NPC
    //         }
    //     }
    //     else
    //     {
    //         Debug.LogError("NpcManager not found in the scene!");
    //     }
    // }

    // public bool CheckItem()
    // {
    //     // ดึงรายการ ItemID ที่ผู้เล่นเลือกไว้
    //     List<string> selectedItemIDs = orderManager.GetSelectedItemIDs();

    //     // ดึงรายการสินค้า NPC
    //     List<ShopItem> npcItems = new List<ShopItem>();
    //     NpcManager npcManager = FindObjectOfType<NpcManager>();
    //     if (npcManager != null)
    //     {
    //         npcItems = npcManager.GetItemToRandom();
    //     }
    //     else
    //     {
    //         Debug.LogError("NpcManager not found in the scene!");
    //         return false; // ส่งคืนค่า false หากไม่พบ NpcManager
    //     }

    //     // ตรวจสอบว่า ItemID ทั้งหมดตรงกันหรือไม่
    //     if (selectedItemIDs.Count != npcItems.Count)
    //     {
    //         Debug.LogWarning("จำนวนสินค้าไม่ตรงกัน!");
    //         ShowIncorrectPrefab();
    //         orderManager.ClearPanel();
    //         return false; // ส่งคืนค่า false หากไม่ตรงกัน
    //     }

    //     for (int i = 0; i < selectedItemIDs.Count; i++)
    //     {
    //         if (selectedItemIDs[i] != npcItems[i].ItemID)
    //         {
    //             Debug.LogWarning($"สินค้าไม่ตรงกัน: {selectedItemIDs[i]} ไม่ตรงกับ {npcItems[i].ItemID}");
    //             ShowIncorrectPrefab();
    //             orderManager.ClearPanel();
    //             return false; // ส่งคืนค่า false หากสินค้าไม่ตรงกัน
    //         }
    //     }

    //     // หากตรงกันทั้งหมด
    //     Debug.Log("สินค้าในรายการตรงกันทั้งหมด!");
    //     orderManager.ClearPanel();
    //     return true; // ส่งคืนค่า true หากสินค้าในรายการตรงกันทั้งหมด
    // }

    // // แสดง prefabIncorrect หากสินค้าไม่ตรงกัน
    // private void ShowIncorrectPrefab()
    // {
    //     if (prefabIncorrect != null)
    //     {
    //         Instantiate(prefabIncorrect, transform.position, Quaternion.identity);
    //     }
    //     else
    //     {
    //         Debug.LogError("Prefab Incorrect ไม่ได้ถูกตั้งค่าไว้!");
    //     }
    // }


}
