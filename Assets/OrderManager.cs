using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [Header("Display Settings")]
    public Transform displayArea;           // พื้นที่สำหรับแสดง prefab
    public GameObject orderPanelPrefab;     // Prefab ที่มี OrderPanelDisplay component
    private OwnerShop ownerShop;

    public GameObject npcGameObject;  // ตัวแปรที่เก็บ NPC GameObject
    private NpcManager npcManager; // เพิ่มการอ้างอิงถึง NpcManager
    private List<GameObject> activeOrders = new List<GameObject>(); // เก็บรายการ GameObjects ที่สร้างขึ้น
    private List<string> selectedItems = new List<string>(); // เก็บรายการ itemIDs
    private List<string> selectedItemIDs = new List<string>(); // เก็บเฉพาะ ItemIDs


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
    public List<string> SelectOrder(string itemID)
    {
        if (orderPanelPrefab == null)
        {
            Debug.LogError("Order Panel Prefab is not assigned!");
            return selectedItems;
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
            return selectedItems;
        }

        // อัพเดทการแสดงผล
        display.UpdateDisplay(itemID);

        // เพิ่ม prefab ที่สร้างขึ้นไปในลิสต์
        activeOrders.Add(displayObj);

        // เพิ่ม itemID ไปในลิสต์ selectedItems
        selectedItems.Add(itemID);

        // แสดงค่า itemID ที่ผู้เล่นเลือกใน Console
        Debug.Log("Item selected: " + itemID);

        // คืนค่า selectedItems
        return selectedItems;
    }

    public List<string> GetSelectedItemIDs()
    {
        return new List<string>(selectedItemIDs); // คืนค่าลิสต์ของ ItemIDs เพื่อรอการเปรียบเทียบ
    }

     // ฟังก์ชันนี้สามารถใช้งานใน Unity Inspector ผ่าน OnClick
    public void OnSelectOrder(string itemID)
    {
        SelectOrder(itemID);
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
}
