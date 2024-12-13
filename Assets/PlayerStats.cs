using System.Collections.Generic;
using UnityEngine;
using TMPro; // สำหรับ UI

// สร้าง Base Class เพื่อรองรับการขยายตัว
public abstract class PlayerBase
{
    public float TotalMoney { get; protected set; }
    public float Expenses { get; protected set; }
    public Dictionary<string, int> Inventory { get; protected set; } = new Dictionary<string, int>();

    public abstract void AddMoney(float amount);
    public abstract bool SpendMoney(float amount);
    public abstract void AddItem(string itemName, int quantity = 1);
    public abstract bool RemoveItem(string itemName, int quantity = 1);
}

// ใช้ Composition แทนการสืบทอดจาก MonoBehaviour โดยตรง
public class PlayerStats : MonoBehaviour
{
    [Header("Money Settings")]
    public float startingMoney;
    public float remainmoney;

    [Header("UI References")]
    public TMP_Text moneyText;
    public TMP_Text expensestext;

    public int Hearts { get; private set; } = 3;

    private PlayerBase playerBase;

    public float RemaninMoney => playerBase.TotalMoney; // เปลี่ยนชื่อ getter ให้ตรงกับโค้ดเดิม
    public float Expenses => playerBase.Expenses; // ยืนยันการเข้าถึง Expenses

    private void Awake()
    {
        playerBase = new ConcretePlayerBase(startingMoney); // ใช้คลาส ConcretePlayerBase
    }

    private void Start()
    {
        UpdateMoneyDisplay(); // แสดงผลเงินตอนเริ่มต้น
    }

    public void AddMoney(float amount)
    {
        playerBase.AddMoney(amount);
        UpdateMoneyDisplay();
    }

    public bool SpendMoney(float amount)
    {
        bool result = playerBase.SpendMoney(amount);
        if (result)
        {
            UpdateMoneyDisplay();
            UpdateExpensesDisplay();
        }
        return result;
    }

    public void AddItem(string itemName, int quantity = 1)
    {
        playerBase.AddItem(itemName, quantity);
    }

    public bool RemoveItem(string itemName, int quantity = 1)
    {
        return playerBase.RemoveItem(itemName, quantity);
    }

    public bool HasItem(string itemName, int requiredQuantity)
    {
        return playerBase.Inventory.ContainsKey(itemName) && playerBase.Inventory[itemName] >= requiredQuantity;
    }

    public void UpdateMoneyDisplay()
    {
        if (moneyText != null)
        {
            moneyText.text = "Total : " + playerBase.TotalMoney.ToString("F0");
        }
        else
        {
            Debug.LogWarning("MoneyText is not assigned!");
        }
    }

    public void UpdateExpensesDisplay()
    {
        if (expensestext != null)
        {
            expensestext.text = "Total expenses : " + playerBase.Expenses.ToString("F0");
        }
        else
        {
            Debug.LogWarning("ExpensesText is not assigned!");
        }
    }

    public void LogTransaction(string itemName, float price)
    {
        Debug.Log($"Bought {itemName} for {price}. Remaining money: {playerBase.TotalMoney}");
    }

    public bool BuyItemFromShop(Shop shop, string itemName)
    {
        if (shop == null)
        {
            Debug.LogError("Shop reference is null!");
            return false;
        }

        Shop.ShopItem item = shop.GetItem(itemName);
        if (item == null)
        {
            Debug.LogWarning("Item not found in shop!");
            return false;
        }

        if (SpendMoney(item.Price))
        {
            AddItem(item.Name);
            Debug.Log($"Bought {item.Name} for {item.Price}. Remaining money: {playerBase.TotalMoney}");
            return true;
        }
        else
        {
            Debug.LogWarning("Not enough money!");
            return false;
        }
    }

    public bool BuyShop(string shopName, float shopPrice)
    {
        if (SpendMoney(shopPrice))
        {
            Debug.Log($"You are now the owner of {shopName}!");
            return true;
        }
        Debug.LogWarning("Not enough money to buy the shop!");
        return false;
    }
}

// คลาส ConcretePlayerBase สำหรับการใช้งานจริง
public class ConcretePlayerBase : PlayerBase
{
    public ConcretePlayerBase(float startingMoney)
    {
        TotalMoney = startingMoney;
    }

    public override void AddMoney(float amount)
    {
        TotalMoney += amount;
    }

    public override bool SpendMoney(float amount)
    {
        if (TotalMoney >= amount)
        {
            TotalMoney -= amount;
            Expenses += amount;
            return true;
        }
        return false;
    }

    public override void AddItem(string itemName, int quantity = 1)
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

    public override bool RemoveItem(string itemName, int quantity = 1)
    {
        if (Inventory.ContainsKey(itemName) && Inventory[itemName] >= quantity)
        {
            Inventory[itemName] -= quantity;
            if (Inventory[itemName] <= 0)
                Inventory.Remove(itemName);
            return true;
        }
        return false;
    }
}