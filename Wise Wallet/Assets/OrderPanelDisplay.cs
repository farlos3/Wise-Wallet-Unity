using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OrderPanelDisplay : MonoBehaviour
{
    [Header("UI Elements")]
    public Image itemImage;         // รูปภาพของสินค้า
    public Text itemNameText;       // ชื่อของสินค้า
    public TMP_Text itemPriceText;  // ราคาของสินค้า

    private OwnerShop ownerShop; // อ้างอิงถึง OwnerShop

    private void Awake()
    {
        // ลาก OwnerShop จากสภาพแวดล้อม
        ownerShop = FindObjectOfType<OwnerShop>();
        if (ownerShop == null)
        {
            Debug.LogError("OwnerShop not found in the scene!");
        }
    }

    public void UpdateDisplay(string itemID)
    {
        if (ownerShop == null)
        {
            Debug.LogError("OwnerShop is not assigned!");
            return;
        }

        // ดึงข้อมูลสินค้าจาก OwnerShop
        ShopItem item = ownerShop.GetItemByID(itemID);

        if (item != null)
        {
            // เปลี่ยนรูปภาพ
            if (itemImage != null)
            {
                itemImage.sprite = item.ItemImage;
            }

            // เปลี่ยนชื่อสินค้า
            if (itemNameText != null)
            {
                itemNameText.text = item.ItemName;
            }

            // เปลี่ยนราคาสินค้า
            if (itemPriceText != null)
            {
                itemPriceText.text = item.ItemPrice.ToString("F0");
            }
        }
        else
        {
            Debug.LogWarning($"Item with ID {itemID} not found in the shop!");
        }
    }
}
