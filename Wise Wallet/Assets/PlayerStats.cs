using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float TotalMoney { get; private set; } = 300f;
    public int Hearts { get; private set; } = 3;
    public float Expenses { get; private set; } = 0f;
    public Dictionary<string, int> Inventory { get; private set; }

    private void Awake()
    {
        Inventory = new Dictionary<string, int>();
    }

    public void AddMoney(float amount)
    {
        TotalMoney += amount;
    }

    public bool SpendMoney(float amount)
    {
        if (TotalMoney >= amount)
        {
            TotalMoney -= amount;
            Expenses += amount;
            return true;
        }
        Debug.Log("Not enough money!");
        return false;
    }

    public void AddItem(string itemName, int quantity = 1)
    {
        if (Inventory.ContainsKey(itemName))
        {
            Inventory[itemName] += quantity;
        }
        else
        {
            Inventory[itemName] = quantity;
        }
    }

    public bool RemoveItem(string itemName, int quantity = 1)
    {
        if (Inventory.ContainsKey(itemName) && Inventory[itemName] >= quantity)
        {
            Inventory[itemName] -= quantity;
            if (Inventory[itemName] <= 0)
                Inventory.Remove(itemName);
            return true;
        }
        Debug.Log($"Not enough {itemName} in inventory!");
        return false;
    }

    public void LoseHeart()
    {
        if (Hearts > 0)
        {
            Hearts--;
        }
        else
        {
            Debug.Log("No hearts left!");
        }
    }

    public bool HasItem(string itemName, int requiredQuantity)
    {
        return Inventory.ContainsKey(itemName) && Inventory[itemName] >= requiredQuantity;
    }

    public bool BuyItemFromShop(Shop shop, string itemName)
    {
        if (shop == null)
        {
            Debug.LogError("Shop reference is null!");
            return false;
        }

        Shop.ShopItem item = shop.GetItem(itemName); // ดึงข้อมูลสินค้า
        if (item == null)
        {
            Debug.LogWarning("Item not found in shop!");
            return false;
        }

        // ตรวจสอบว่าเงินพอที่จะซื้อหรือไม่
        if (TotalMoney >= item.Price)
        {
            TotalMoney -= item.Price; // หักเงินจากผู้เล่น
            AddItem(item.Name); // เพิ่มสินค้าในคลัง


            Debug.Log($"Bought {item.Name} for {item.Price}. Remaining money: {TotalMoney}");
            return true;
        }
        else
        {
            Debug.LogWarning("Not enough money!");
            return false;
        }
    }
}
