using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderPanelManager : MonoBehaviour
{
    public GameObject orderPanel; // Panel ที่จะแสดงสินค้า
    public Transform displayContainer; // จุดแสดงผลของสินค้าใน Order Panel
    public GameObject itemPrefab; // Prefab สำหรับแสดงสินค้าใน Order Panel

    void Start()
    {
        if (displayContainer == null)
        {
            Debug.LogError("Display Container is not assigned in the Order Panel!");
        }

        if (itemPrefab == null)
        {
            Debug.LogError("Item Prefab is not assigned in the Order Panel!");
        }
    }

    // แสดงสินค้าใน Order Panel
    public void DisplayItemInOrderPanel(GameObject itemPrefab)
    {
        if (displayContainer == null)
        {
            Debug.LogError("Display Container is not assigned in the Order Panel!");
            return;
        }

        // สร้างไอคอนสินค้าจาก Prefab ใน Order Panel
        GameObject itemObject = Instantiate(itemPrefab, displayContainer);

        // ใช้สคริปต์ ShopItemDisplay เพื่อกำหนดค่าให้กับสินค้า
        ShopItemDisplay shopItemDisplay = itemObject.GetComponent<ShopItemDisplay>();
        if (shopItemDisplay != null)
        {
            Item item = itemPrefab.GetComponent<Item>(); // ตรวจสอบและดึงข้อมูลสินค้า
            shopItemDisplay.Setup(item); // ตั้งค่าภาพและราคา
        }
        else
        {
            Debug.LogError("ShopItemDisplay component not found on the instantiated item!");
        }
    }


    // เคลียร์สินค้าจาก Order Panel
    private void ClearOrderPanel()
    {
        foreach (Transform child in displayContainer) // ลบทุก child ใน Order Panel
        {
            Destroy(child.gameObject);
        }
    }

    // ยืนยันการสั่งซื้อสินค้า
    private void SubmitOrder()
    {
        // เคลียร์สินค้าจาก Order Panel
        ClearOrderPanel();
        Debug.Log("Order confirmed and item added to inventory.");
    }
}
