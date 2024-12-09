using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [Header("Display Settings")]
    public Transform displayArea;           // พื้นที่สำหรับแสดง prefab
    public GameObject orderPanelPrefab;     // Prefab ที่มี OrderPanelDisplay component
    
    private OwnerShop ownerShop;

    private void Awake()
    {
        ownerShop = FindObjectOfType<OwnerShop>();
        if (ownerShop == null)
        {
            Debug.LogError("OwnerShop not found in the scene!");
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
    }
}