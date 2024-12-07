using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItemDisplay : MonoBehaviour
{
    public Text itemNameText;           // UI Text for item name
    public GameObject itemPriceObject;  // UI GameObject for item price
    public Image itemImage;             // UI Image for item icon

    // Function to set item data in the UI
    public void SetItem(string name, float price, Sprite icon, GameObject priceObject)
    {
        if (itemNameText != null)
            itemNameText.text = name; // Set item name text

        if (itemImage != null)
            itemImage.sprite = icon; // Set item icon

        if (itemPriceObject != null)
        {
            itemPriceObject.SetActive(true);

            var priceText = itemPriceObject.GetComponentInChildren<TextMeshProUGUI>();
            if (priceText != null)
            {
                priceText.text = price.ToString("F0"); // Format price as currency
            }
            else
            {
                Debug.LogWarning("ItemPriceObject does not contain a TextMeshProUGUI component!");
            }
        }
        else
        {
            Debug.LogWarning("ItemPriceObject is null!");
        }

        if (priceObject != null)
        {
            priceObject.SetActive(true);
            var additionalPriceText = priceObject.GetComponentInChildren<TextMeshProUGUI>();
            if (additionalPriceText != null)
            {
                additionalPriceText.text = price.ToString("C");
            }
        }
        else
        {
            Debug.LogWarning("PriceObject is null!");
        }

        Debug.Log($"SetItem called for: {name} with price: {price} and icon: {icon?.name}");
    }

    // ปิดการแสดงผลของราคาสินค้าโดยซ่อน itemPriceObject
    public void HidePrice()
    {
        if (itemPriceObject != null)
            itemPriceObject.SetActive(false);
    }

    // Function to reset display
    public void ResetDisplay()
    {
        if (itemNameText != null)
            itemNameText.text = string.Empty;

        if (itemImage != null)
            itemImage.sprite = null;

        if (itemPriceObject != null)
        {
            itemPriceObject.SetActive(false);

            var priceText = itemPriceObject.GetComponentInChildren<TextMeshProUGUI>();
            if (priceText != null)
            {
                priceText.text = string.Empty;
            }
        }
    }
}
