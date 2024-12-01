using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MissionItemDisplay : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text itemNameText;       // ช่องแสดงชื่อสินค้า
    public TMP_Text priceText;          // ช่องแสดงราคา
    public Image itemIconImage;         // รูปภาพสินค้า
    public GameObject checkmarkIcon;    // ไอคอนแสดงสถานะสำเร็จ

    private Shop shop; // ตัวแปรเชื่อมกับ Shop


    public void SetMission(string itemName, float price, Sprite itemIcon)
    {
        if (itemNameText != null)
        {
            itemNameText.text = itemName; // แสดงชื่อสินค้า
        }

        if (priceText != null)
        {
            priceText.text = price.ToString("F0"); // แสดงราคา
        }

        if (itemIconImage != null && itemIcon != null)
        {
            itemIconImage.sprite = itemIcon; // แสดงไอคอนสินค้า
        }

        if (checkmarkIcon != null)
        {
            checkmarkIcon.SetActive(false); // ซ่อนไว้ตอนเริ่มต้น
        }
    }

    // อัปเดตสถานะสำเร็จ
    public void MarkAsCompleted()
    {
        if (checkmarkIcon != null)
        {
            checkmarkIcon.SetActive(true);
        }
    }
}
