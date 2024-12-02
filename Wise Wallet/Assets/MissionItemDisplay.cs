using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MissionItemDisplay : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text itemNameText;       // ช่องแสดงชื่อสินค้า
    public TMP_Text priceText;          // ช่องแสดงราคา
    public Image itemIconImage;         // รูปภาพสินค้า
    public GameObject checkmarkIcon;    // ไอคอนแสดงสถานะสำเร็จ (เริ่มต้น)

    public Sprite defaultCheckmarkSprite;  // Sprite ที่ใช้เป็นรูป checkmark เริ่มต้น

    private Shop shop; // ตัวแปรเชื่อมกับ Shop

    // ฟังก์ชันตั้งค่าเริ่มต้นของภารกิจ
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

        // แสดง checkmarkIcon ในสถานะเริ่มต้น
        if (checkmarkIcon != null)
        {
            checkmarkIcon.SetActive(true);  // ให้ checkmark แสดงอยู่ตอนเริ่มต้น
            var imageComponent = checkmarkIcon.GetComponent<Image>();
            if (imageComponent != null && defaultCheckmarkSprite != null)
            {
                imageComponent.sprite = defaultCheckmarkSprite; // ตั้งค่าเป็น Sprite เริ่มต้น
            }
        }
    }

    // ฟังก์ชันอัปเดตสถานะเมื่อภารกิจสำเร็จ
    public void MarkAsCompleted(Sprite checkmarkSprite)
    {
        if (checkmarkIcon != null)
        {
            var imageComponent = checkmarkIcon.GetComponent<Image>();
            if (imageComponent != null && checkmarkSprite != null)
            {
                imageComponent.sprite = checkmarkSprite; // เปลี่ยน Sprite เป็น checkmark ที่ส่งเข้าไป
            }
        }
    }
}
