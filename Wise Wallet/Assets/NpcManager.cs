using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcManager : MonoBehaviour
{
    // ลิสต์ชื่อร้านที่ NPC สามารถเลือกได้
    public List<string> shopNames; // ชื่อร้านที่ต้องการใช้
    public Transform itemImageTransform1; // ตำแหน่งรูปภาพสินค้า 1
    public Transform itemImageTransform2; // ตำแหน่งรูปภาพสินค้า 2
    public Transform itemImageTransform3; // ตำแหน่งรูปภาพสินค้า 3


    private Dictionary<string, OwnerShop> ownerShops = new Dictionary<string, OwnerShop>();

    private void Start()
    {
        // เก็บ OwnerShop ทั้งหมดในฉากที่มีชื่อร้านตรงกับ shopNames
        OwnerShop[] shopsInScene = FindObjectsOfType<OwnerShop>();
        foreach (OwnerShop shop in shopsInScene)
        {
            if (shopNames.Contains(shop.ShopName))
            {
                ownerShops[shop.ShopName] = shop; // เก็บข้อมูลร้านใน Dictionary โดยใช้ชื่อร้านเป็น key
            }
        }

        // หากไม่มีร้านใน Dictionary จะมีการแจ้งเตือน
        if (ownerShops.Count == 0)
        {
            Debug.LogError("ไม่พบร้านที่ตรงกับชื่อที่ระบุใน shopNames");
        }
    }

    // ฟังก์ชันสุ่มสินค้าจากร้านค้าที่เลือก
    public void GetItemToRandom()
    {
        foreach (string shopName in shopNames) // วนลูปไปตามชื่อร้านที่กรอกใน shopNames
        {
            // ค้นหาร้านจากชื่อร้านที่ตรงกับ GameObject ในฉาก
            GameObject shopGameObject = GameObject.Find(shopName);

            if (shopGameObject != null)
            {
                OwnerShop ownerShop = shopGameObject.GetComponent<OwnerShop>();
                if (ownerShop != null)
                {
                    // สุ่มสินค้าจากร้านที่พบ
                    List<ShopItem> randomItems = new List<ShopItem>();
                    if (ownerShop.ShopItems.Count > 0)
                    {
                        List<int> selectedIndices = new List<int>();
                        while (selectedIndices.Count < 3)
                        {
                            int randomIndex = Random.Range(0, ownerShop.ShopItems.Count);
                            if (!selectedIndices.Contains(randomIndex))
                            {
                                selectedIndices.Add(randomIndex);
                                randomItems.Add(ownerShop.ShopItems[randomIndex]);
                            }
                        }

                        // อัปเดตภาพใน Transform ที่เกี่ยวข้อง
                        UpdateImageInTransform(itemImageTransform1, randomItems[0].ItemImage);
                        UpdateImageInTransform(itemImageTransform2, randomItems[1].ItemImage);
                        UpdateImageInTransform(itemImageTransform3, randomItems[2].ItemImage);
                    }
                    else
                    {
                        Debug.Log($"ร้าน {shopName} ไม่มีสินค้าครับ");
                    }
                }
                else
                {
                    Debug.LogWarning($"ไม่พบ OwnerShop ในร้าน: {shopName}");
                }
            }
            else
            {
                Debug.LogWarning($"ไม่พบ GameObject ที่มีชื่อ {shopName}");
            }
        }
    }


    // ฟังก์ชันที่จะอัปเดตรูปภาพใน Transform ที่ระบุ
    private void UpdateImageInTransform(Transform itemTransform, Sprite newImage)
    {
        if (itemTransform != null)
        {
            // ค้นหา Image component ใน Transform
            Image imageComponent = itemTransform.GetComponent<Image>();
            if (imageComponent != null)
            {
                Debug.Log($"Updating Sprite: {newImage.name}"); // ตรวจสอบชื่อ Sprite
                imageComponent.sprite = newImage;  // เปลี่ยนรูปภาพ
            }
            else
            {
                Debug.LogError("Image component not found in the Transform!");
            }
        }
        else
        {
            Debug.LogError("Transform is null!");
        }
    }
}
