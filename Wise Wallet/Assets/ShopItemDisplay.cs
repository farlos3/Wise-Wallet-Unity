using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemDisplay : MonoBehaviour
{
    public Image itemImage; // Image component for displaying the item's image
    public TMP_Text itemPriceText; // Text component for displaying the item's price

    // ฟังก์ชันสำหรับการตั้งค่าใน prefab
    public void Setup(Item item)
    {
        if (itemImage != null)
        {
            itemImage.sprite = item.itemImage; // เปลี่ยนรูปภาพให้ตรงกับสินค้า
        }

        if (itemPriceText != null)
        {
            itemPriceText.text = item.itemPrice.ToString("F2"); // ตั้งราคาสินค้า
        }
    }
}
