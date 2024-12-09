using UnityEngine;
using UnityEngine.UI;

public class NpcDisplay : MonoBehaviour
{
    [Header("UI Elements")]
    public Image itemImage; // รูปภาพของสินค้า

    public void UpdateDisplay(string itemID, Sprite itemImage)
    {
        if (itemImage != null)
        {
            // แสดงข้อความใน Console เพื่อ Debug
            Debug.Log($"Updating NPC Display with ItemID: {itemID}, ItemImage: {itemImage.name}");

            // เปลี่ยนรูปภาพใน UI
            if (this.itemImage != null)
            {
                this.itemImage.sprite = itemImage;
            }
        }
        else
        {
            Debug.LogWarning($"ItemImage is null for ItemID: {itemID}!");
        }
    }
}
