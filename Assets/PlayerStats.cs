using UnityEngine;
using System.Collections.Generic; // เพิ่มบรรทัดนี้
using UnityEngine.UI;  // สำหรับ UI.Text
using TMPro;  // ถ้าใช้ TextMeshProUGUI

public class PlayerStats : MonoBehaviour
{
    [Header("Money Settings")]
    public float startingMoney;  // เงินเริ่มต้นที่สามารถตั้งค่าได้ใน Unity Inspector
    public float remainmoney;  // เงินเริ่มต้นที่สามารถตั้งค่าได้ใน Unity Inspector

    public float TotalMoney { get; private set; } // เงินที่ผู้เล่นมีในปัจจุบัน
    public float RemaninMoney { get; private set; } 


    [Header("UI References")]
    public TMP_Text moneyText; // ถ้าใช้ TextMeshProUGUI
    public TMP_Text expensestext;

    public int Hearts { get; private set; } = 3;
    public float Expenses { get; private set; } = 0f;
    public Dictionary<string, int> Inventory { get; private set; }

    private void Awake()
    {
        Inventory = new Dictionary<string, int>();
        TotalMoney = startingMoney;  // ตั้งค่าเงินเริ่มต้นจาก Inspector
        RemaninMoney = remainmoney;
    }

    private void Start()
    {
        UpdateMoneyDisplay();  // แสดงผลเงินตอนเริ่มต้น
    }

    // ฟังก์ชั่นอัปเดตการแสดงผลเงิน
    public void UpdateMoneyDisplay()
    {
        if (moneyText != null)
        {
            moneyText.text = "Total : " + TotalMoney.ToString("F0");
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
        expensestext.text = "Total expenses : " + Expenses.ToString("F0");
    }
    else
    {
        Debug.LogWarning("ExpensesText is not assigned!");
    }
}

    // เมื่อมีการใช้เงิน
    public bool SpendMoney(float amount)
    {
        if (TotalMoney >= amount)
        {
            TotalMoney -= amount;
            Expenses += amount;
            UpdateMoneyDisplay();  // อัปเดตการแสดงผลเงิน
            UpdateExpensesDisplay();
            return true;
        }
        Debug.Log("Not enough money!");
        return false;
    }

    // เพิ่มเงิน
    public void AddMoney(float amount)
    {
        TotalMoney += amount;
        UpdateMoneyDisplay();  // อัปเดตการแสดงผลเงิน
    }

    // ฟังก์ชั่นสำหรับการแสดงรายการสินค้าหรือทำการซื้อ
    public void LogTransaction(string itemName, float price)
    {
        Debug.Log($"Bought {itemName} for {price}. Remaining money: {TotalMoney}");
    }

    // ฟังก์ชั่นซื้อสินค้า
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
            Expenses += item.Price;

            AddItem(item.Name); // เพิ่มสินค้าในคลัง

            UpdateMoneyDisplay();  // อัปเดตการแสดงผลเงิน
            UpdateExpensesDisplay();
            Debug.Log($"Bought {item.Name} for {item.Price}. Remaining money: {TotalMoney}");
            return true;
        }
        else
        {
            Debug.LogWarning("Not enough money!");
            return false;
        }
    }

    // เพิ่มไอเทมในคลัง
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

    // ลบไอเทมจากคลัง
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

    // ตรวจสอบว่ามีไอเทมในคลังหรือไม่
    public bool HasItem(string itemName, int requiredQuantity)
    {
        return Inventory.ContainsKey(itemName) && Inventory[itemName] >= requiredQuantity;
    }
}
