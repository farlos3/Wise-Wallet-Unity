using UnityEngine;
using UnityEngine.UI;

public class NpcDisplay : MonoBehaviour
{
    // ฟิลด์สำหรับ GameObject ที่จะใช้เปลี่ยนรูปภาพ
    public Image itemImage1;  // สำหรับรูปภาพของสินค้า 1
    public Image itemImage2;  // สำหรับรูปภาพของสินค้า 2
    public Image itemImage3;  // สำหรับรูปภาพของสินค้า 3

    // ฟังก์ชัน UpdateDisplay ที่จะรับข้อมูลและอัปเดตภาพ
    public void UpdateDisplay(Sprite itemImage1Sprite, Sprite itemImage2Sprite, Sprite itemImage3Sprite)
    {
        // เปลี่ยนรูปภาพในแต่ละตำแหน่ง
        if (itemImage1 != null)
        {
            itemImage1.sprite = itemImage1Sprite;  // เปลี่ยนรูปภาพของสินค้าที่ 1
        }
        
        if (itemImage2 != null)
        {
            itemImage2.sprite = itemImage2Sprite;  // เปลี่ยนรูปภาพของสินค้าที่ 2
        }
        
        if (itemImage3 != null)
        {
            itemImage3.sprite = itemImage3Sprite;  // เปลี่ยนรูปภาพของสินค้าที่ 3
        }
    }
}
