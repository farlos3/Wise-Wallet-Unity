using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcManager : MonoBehaviour
{
    private List<ShopItem> itemsFromShop = new List<ShopItem>();

    [Header("UI References")]
    [SerializeField] private Image position1;
    [SerializeField] private Image position2;
    [SerializeField] private Image position3;

    public void GetItemFromShop(string shopName)
    {
        OwnerShop selectedShop = System.Array.Find(FindObjectsOfType<OwnerShop>(), shop => shop.ShopName == shopName);

        if (selectedShop == null)
        {
            Debug.LogError($"Shop with name '{shopName}' not found!");
            return;
        }

        itemsFromShop = new List<ShopItem>(selectedShop.ShopItems);
        Debug.Log($"Retrieved {itemsFromShop.Count} items from {selectedShop.ShopName}.");
    }

    public void PrintItems()
    {
        foreach (var item in itemsFromShop)
        {
            Debug.Log($"Item Name: {item.ItemName}, Price: {item.ItemPrice}, ID: {item.ItemID}");
        }
    }

    public void RandomItem()
    {
        if (itemsFromShop.Count < 3)
        {
            Debug.LogError("Not enough items in the shop to select 3 unique items.");
            return;
        }

        // Verify UI components are properly assigned
        if (!VerifyUIComponents())
        {
            return;
        }

        List<ShopItem> randomItems = new List<ShopItem>();
        List<int> selectedIndices = new List<int>();

        // Select random items
        while (randomItems.Count < 3)
        {
            int randomIndex = Random.Range(0, itemsFromShop.Count);
            if (!selectedIndices.Contains(randomIndex))
            {
                selectedIndices.Add(randomIndex);
                randomItems.Add(itemsFromShop[randomIndex]);
                Debug.Log($"Selected item at index {randomIndex}: ID={itemsFromShop[randomIndex].ItemID}");
            }
        }

        // Update UI Images
        UpdateUIImage(position1, randomItems[0], 1);
        UpdateUIImage(position2, randomItems[1], 2);
        UpdateUIImage(position3, randomItems[2], 3);
    }

    private bool VerifyUIComponents()
    {
        if (position1 == null)
        {
            Debug.LogError("Position1 Image component is not assigned!");
            return false;
        }
        if (position2 == null)
        {
            Debug.LogError("Position2 Image component is not assigned!");
            return false;
        }
        if (position3 == null)
        {
            Debug.LogError("Position3 Image component is not assigned!");
            return false;
        }
        return true;
    }

    private void UpdateUIImage(Image position, ShopItem item, int positionNumber)
    {
        if (item == null)
        {
            Debug.LogError($"Item for position {positionNumber} is null!");
            return;
        }

        if (item.ItemImage == null)
        {
            Debug.LogError($"ItemImage is null for item ID: {item.ItemID} at position {positionNumber}");
            return;
        }

        position.sprite = item.ItemImage;
        Debug.Log($"Successfully updated position {positionNumber} with Item ID: {item.ItemID}");
    }
}