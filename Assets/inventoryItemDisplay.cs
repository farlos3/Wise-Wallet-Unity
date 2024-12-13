using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItemDisplay : MonoBehaviour
{
    public Text itemNameText;           // UI Text for item name
    public GameObject itemPriceObject;  // UI GameObject for item price
    public Image itemImage;             // UI Image for item icon

    // Set item data in the UI
    public void SetItem(string name, float price, Sprite icon)
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
        }
    }

    // Hide the price if needed
    public void HidePrice()
    {
        if (itemPriceObject != null)
            itemPriceObject.SetActive(false);
    }

    // Reset display
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