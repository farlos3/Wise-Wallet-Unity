using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [Header("Display Settings")]
    public Transform displayArea;           // พื้นที่สำหรับแสดง prefab
    public GameObject orderPanelPrefab;     // Prefab ที่มี OrderPanelDisplay component
    private List<string> selectedItemIDs = new List<string>();  // เก็บรายการที่ผู้เล่นเลือก

    private OwnerShop ownerShop;

    public GameObject npcGameObject;  // ตัวแปรที่เก็บ NPC GameObject
    private NpcManager npcManager; // เพิ่มการอ้างอิงถึง NpcManager
    private List<GameObject> activeOrders = new List<GameObject>();  // ลิสต์ที่เก็บ prefab ที่สร้างขึ้น
    private List<string> randomItemIDs = new List<string>(); // ลิสต์เก็บ ItemID ที่สุ่มจาก GetItemsFromShop
    public GameObject incorrectOrderPanelPrefab; // เพิ่มตัวแปรนี้ในฟิลด์ของคลาส

    private void Awake()
    {
        ownerShop = FindObjectOfType<OwnerShop>();
        if (ownerShop == null)
        {
            Debug.LogError("OwnerShop not found in the scene!");
        }

        npcManager = FindObjectOfType<NpcManager>();  // หา NpcManager ใน Scene
        if (npcManager == null)
        {
            Debug.LogError("NpcManager not found in the scene!");
        }

    }

    public void SelectOrder(string itemID)
    {
        if (orderPanelPrefab == null)
        {
            Debug.LogError("Order Panel Prefab is not assigned!");
            return;
        }

        // สร้าง prefab ในพื้นที่ที่กำหนด
        GameObject displayObj;
        if (displayArea != null)
        {
            displayObj = Instantiate(orderPanelPrefab, displayArea);
        }
        else
        {
            displayObj = Instantiate(orderPanelPrefab);
            Debug.LogWarning("Display area not set, instantiating at world origin");
        }

        // ดึง component OrderPanelDisplay และอัพเดทข้อมูล
        OrderPanelDisplay display = displayObj.GetComponent<OrderPanelDisplay>();
        if (display == null)
        {
            Debug.LogError("OrderPanelDisplay component not found on prefab!");
            Destroy(displayObj);
            return;
        }

        // อัพเดทการแสดงผล
        display.UpdateDisplay(itemID);

        // เพิ่ม prefab ที่สร้างขึ้นไปในลิสต์
        activeOrders.Add(displayObj);

        // แสดงค่า itemID ที่ผู้เล่นเลือกใน Console
        Debug.Log("Item selected: " + itemID);  // เพิ่มคำสั่งนี้เพื่อแสดงค่า itemID
    }

    public void ClearPanel()
    {
        foreach (GameObject order in activeOrders)
        {
            if (order != null)
            {
                Destroy(order); // ทำลาย prefab
            }
        }

        // ล้างลิสต์หลังจากลบแล้ว
        activeOrders.Clear();
    }

//     public void OnButtonClick()
//     {
//         bool isCorrect = CheckOrder(); // เรียกฟังก์ชันที่มีการส่งค่ากลับ
//         if (isCorrect)
//         {
//             Debug.Log("Order is correct!");
//         }
//         else
//         {
//             Debug.Log("Order is incorrect!");
//         }
//     }
}
