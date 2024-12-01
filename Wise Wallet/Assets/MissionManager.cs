using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionManager : MonoBehaviour
{
    [System.Serializable]
    public class MissionItem
    {
        public string Name;
        public float Price;
        public Sprite Icon;
        public int RequiredQuantity;

        public MissionItem(string name,float price, Sprite icon, int requiredQuantity = 1)
        {
            Name = name;
            Price = price;
            Icon = icon;
            RequiredQuantity = requiredQuantity;
        }
    }

    [Header("Mission Settings")]
    public GameObject missionPrefab; // Prefab สำหรับแสดงเป้าหมายภารกิจ
    public Transform missionDisplayContainer; // จุดแสดงผลภารกิจในหน้าต่าง
    public TMP_Text remainingMoneyText; // ช่องแสดงเงินที่ควรเหลือ
    public TMP_Text expensesText; // ช่องแสดงเงินที่ใช้ไป
    public GameObject missionCompleteMessage; // แสดงข้อความ "Mission Complete"

    [SerializeField]
    private List<MissionItem> missionItems = new List<MissionItem>(); // รายการเป้าหมายภารกิจ
    private Shop shop; // ตัวแปรเชื่อมกับ Shop
    private PlayerStats playerStats; // ตัวแปรเชื่อมกับ PlayerStats

    private void Awake()
    {
        missionCompleteMessage.SetActive(false); // ซ่อนข้อความ Mission Complete ตอนเริ่มเกม
        shop = FindObjectOfType<Shop>();
        playerStats = FindObjectOfType<PlayerStats>(); // เชื่อมโยงกับ PlayerStats

        if (shop == null)
        {
            Debug.LogError("Shop is not found in the scene!");
        }
    }

    private void Start()
    {
        DisplayMissionItems();
        UpdateMissionStatus();
    }

    // สร้างรายการภารกิจ
    public void DisplayMissionItems()
    {
        foreach (var missionItem in missionItems)
        {
            if (missionPrefab == null || missionDisplayContainer == null)
            {
                Debug.LogError("MissionPrefab or MissionDisplayContainer is not assigned in the Inspector!");
                return;
            }

            // ค้นหาข้อมูลสินค้าใน Shop
            Shop.ShopItem item = shop.GetItem(missionItem.Name);
            if (item == null)
            {
                Debug.LogError($"Item not found in shop: {missionItem.Name}");
                continue;
            }

            // สร้าง Prefab ของ Mission Item และใส่ลงใน container
            GameObject missionObject = Instantiate(missionPrefab, missionDisplayContainer);

            // ตั้งค่าข้อมูลใน MissionItemDisplay
            var missionDisplay = missionObject.GetComponent<MissionItemDisplay>();
            if (missionDisplay != null)
            {
                // ส่งข้อมูลสินค้าไปที่ MissionItemDisplay
                missionDisplay.SetMission(missionItem.Name,item.Price, missionItem.Icon);
            }
            else
            {
                Debug.LogError("MissionItemDisplay component is missing in the missionPrefab!");
            }
        }
    }


    // ตรวจสอบสถานะภารกิจ
    public void UpdateMissionStatus()
    {
        bool isMissionComplete = true;

        foreach (Transform child in missionDisplayContainer)
        {
            var missionDisplay = child.GetComponent<MissionItemDisplay>();
            if (missionDisplay != null)
            {
                var missionItem = missionItems.Find(item => item.Name == missionDisplay.itemNameText.text);
                if (missionItem != null)
                {
                    bool isPurchased = playerStats.HasItem(missionItem.Name, missionItem.RequiredQuantity);
                    if (isPurchased)
                    {
                        missionDisplay.MarkAsCompleted();
                    }
                    else
                    {
                        isMissionComplete = false;
                    }
                }
            }
        }

        if (isMissionComplete)
        {
            missionCompleteMessage.SetActive(true);
        }

        if (remainingMoneyText != null)
        {
            remainingMoneyText.text = "Remaining Money: " + playerStats.RemaninMoney.ToString("F0");
        }

        if (expensesText != null)
        {
            expensesText.text = "Expenses: " + playerStats.Expenses.ToString("F0");
        }
    }
}
