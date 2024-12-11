using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShopItem
{
    public string ItemName;        
    public float ItemPrice;        
    public GameObject ItemObject;  
    public Sprite ItemImage;       
    public string ItemID;          
}

public class OwnerShop : MonoBehaviour
{
    [Header("Shop Settings")]
    public string ShopName;             
    public List<ShopItem> ShopItems;    

    [Header("Prefab Settings")]
    public GameObject ItemPrefab;       
    public PlayerStats playerStats;

    private void Awake()
    {
        if (playerStats == null)
        {
            playerStats = FindObjectOfType<PlayerStats>();
            if (playerStats == null)
            {
                Debug.LogError("PlayerStats component not found in the scene!");
            }
        }
    }

    public void AddItem(string name, float price, GameObject obj, Sprite image, string id)
    {
        ShopItem newItem = new ShopItem
        {
            ItemName = name,
            ItemPrice = price,
            ItemObject = obj,
            ItemImage = image,
            ItemID = id
        };
        ShopItems.Add(newItem);
    }

    public ShopItem GetItemByID(string id)
    {
        return ShopItems.Find(item => item.ItemID == id);
    }

    public ShopItem GetItemByName(string name)
    {
        return ShopItems.Find(item => item.ItemName == name);
    }

    public void BuyShop(float shopPrice)
    {
        if (playerStats == null)
        {
            Debug.LogError("PlayerStats reference is null! Make sure PlayerStats exists in the scene.");
            return;
        }

        playerStats.BuyShop(ShopName, shopPrice);
    }
}