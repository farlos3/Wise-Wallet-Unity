using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // สำหรับ UI

public class Managernpc : MonoBehaviour
{
    public GameObject orderPanel; // Panel ที่จะแสดงสินค้า
    public GameObject displayContainer; // GameObject ที่จะแสดงสินค้า (ที่ผู้ใช้จัดเตรียมไว้)
    public Button yesButton; // ปุ่ม "Yes" สำหรับส่งคำสั่งซื้อ

    private PlayerStats player;
    private Shopbuy shop; // เปลี่ยนเป็น Shopbuy type

    private void Start()
    {
        shop = FindObjectOfType<Shopbuy>(); // หา Shopbuy component
        player = FindObjectOfType<PlayerStats>(); // หา PlayerStats component
        yesButton.onClick.AddListener(SubmitOrder); // เพิ่มฟังก์ชันเมื่อกดปุ่ม "Yes"
        RandomlySelectItem(); // เริ่มต้นด้วยการสุ่มเลือกสินค้า
    }

    void RandomlySelectItem()
    {
        // ตรวจสอบว่าผู้เล่นเป็นเจ้าของร้านก่อนสุ่มสินค้า
        if (shop.isOwned)
        {
            Item selectedItem = shop.RandomlySelectItem(); // เรียกฟังก์ชันสุ่มเลือกสินค้าจาก Shopbuy
            if (selectedItem != null)
            {
                DisplayItemInOrderPanel(selectedItem); // แสดงสินค้าใน Order Panel ของ NPC
            }
        }
        else
        {
            Debug.Log("The shop is not owned by the player!");
        }
    }

    void DisplayItemInOrderPanel(Item item)
    {
        if (displayContainer != null)
        {
            // สร้างไอคอนสินค้าจาก Prefab ใน GameObject ที่จัดเตรียมไว้
            GameObject icon = Instantiate(item.itemObject, displayContainer.transform);
            // ตั้งภาพไอคอนเป็นรูปของสินค้าจาก itemImage
            icon.GetComponent<Image>().sprite = item.itemImage;
        }
        else
        {
            Debug.LogError("Display container is not assigned!");
        }
    }

    void SubmitOrder()
    {
        // ตรวจสอบว่าผู้เล่นจัดสินค้าถูกต้อง
        if (player.HasItem(shop.GetSelectedItem().itemName, 1)) // ใช้ GetSelectedItem() แทนที่ selectedItem
        {
            player.AddMoney(shop.GetSelectedItem().itemPrice); // เพิ่มเงินให้ผู้เล่น
            Debug.Log($"NPC purchased {shop.GetSelectedItem().itemName} for {shop.GetSelectedItem().itemPrice}. Player money: {player.TotalMoney}");

            // เคลียร์สินค้าจาก Order Panel
            ClearOrderPanel();
        }
        else
        {
            Debug.Log("Incorrect item arrangement!");
        }
    }

    public void ClearOrderPanel()
    {
        foreach (Transform child in displayContainer.transform) // ลบทุก child ใน Order Panel ของ NPC
        {
            Destroy(child.gameObject);
        }
    }
}
