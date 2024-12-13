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
        public Shop AssociatedShop;
        public int RequiredQuantity;
    }

    [Header("Mission Settings")]
    public GameObject missionPrefab;
    public Transform missionDisplayContainer;
    public TMP_Text remainingMoneyText;
    public TMP_Text expensesText;
    public GameObject missionCompleteMessage;
    public Sprite checkmarkSprite; // รูปภาพ Checkmark ที่จะเปลี่ยนหลังจากตรวจสอบ

    [SerializeField]
    private List<MissionItem> missionItems = new List<MissionItem>();
    private PlayerStats playerStats;

    private void Awake()
    {
        missionCompleteMessage.SetActive(false);
        playerStats = FindObjectOfType<PlayerStats>();

        if (playerStats == null)
        {
            Debug.LogError("PlayerStats is not found in the scene!");
        }
    }

    private void Start()
    {
        DisplayMissionItems();
        UpdateMissionStatus();
    }

    // แสดงรายการภารกิจในหน้าจอ
    public void DisplayMissionItems()
    {
        foreach (var missionItem in missionItems)
        {
            if (missionPrefab == null || missionDisplayContainer == null)
            {
                Debug.LogError("MissionPrefab or MissionDisplayContainer is not assigned in the Inspector!");
                return;
            }

            if (missionItem.AssociatedShop == null)
            {
                Debug.LogError($"Associated shop is not assigned for mission item: {missionItem.Name}");
                continue;
            }

            Shop.ShopItem item = missionItem.AssociatedShop.GetItem(missionItem.Name);
            if (item == null)
            {
                Debug.LogError($"Item not found in shop: {missionItem.Name}");
                continue;
            }

            GameObject missionObject = Instantiate(missionPrefab, missionDisplayContainer);
            var missionDisplay = missionObject.GetComponent<MissionItemDisplay>();
            if (missionDisplay != null)
            {
                missionDisplay.SetMission(missionItem.Name, item.Price, missionItem.Icon);
            }
            else
            {
                Debug.LogError("MissionItemDisplay component is missing in the missionPrefab!");
            }
        }
    }

    // ฟังก์ชันตรวจสอบการซื้อของ item
    public void CheckBuyItem(string itemName)
    {
        bool missionCompleted = false;

        foreach (var missionItem in missionItems)
        {
            if (missionItem.Name == itemName) // ชื่อตรงกับ Mission Item
            {
                foreach (Transform child in missionDisplayContainer)
                {
                    var missionDisplay = child.GetComponent<MissionItemDisplay>();
                    if (missionDisplay != null && missionDisplay.itemNameText.text == itemName)
                    {
                        missionDisplay.MarkAsCompleted(checkmarkSprite); // เปลี่ยน Sprite เมื่อซื้อสำเร็จ
                        Debug.Log($"Checkmark updated for item: {itemName}");
                        missionCompleted = true; // เมื่อซื้อสำเร็จ
                        break; // ออกจาก loop
                    }
                }
            }
        }

        if (missionCompleted)
        {
            UpdateMissionStatus(); // อัปเดตสถานะภารกิจหลังจากซื้อสำเร็จ
        }
        else
        {
            Debug.Log($"Item {itemName} is not part of any mission.");
        }
    }

    public void UpdateMissionStatus()
    {
        bool isMissionComplete = true;

        // ตรวจสอบสถานะของภารกิจทั้งหมด
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
                        // ส่ง checkmarkSprite เข้าไปใน MarkAsCompleted
                        missionDisplay.MarkAsCompleted(checkmarkSprite);
                    }
                    else
                    {
                        isMissionComplete = false; // ถ้าไม่ครบให้ตั้งค่าภารกิจไม่เสร็จ
                    }
                }
            }
        }

        // หากภารกิจทั้งหมดเสร็จสมบูรณ์
        if (isMissionComplete)
        {
            missionCompleteMessage.SetActive(true); // แสดงข้อความว่า "Mission Complete"
        }
        else
        {
            missionCompleteMessage.SetActive(false); // ถ้ายังไม่เสร็จ จะซ่อนข้อความ
        }

        // แสดงข้อมูลเงินที่เหลือ
        if (remainingMoneyText != null)
        {
            remainingMoneyText.text = "Remaining Money: " + playerStats.RemaninMoney.ToString("F0");
        }

        // แสดงข้อมูลค่าใช้จ่าย
        if (expensesText != null)
        {
            expensesText.text = "Expenses: " + playerStats.Expenses.ToString("F0");
        }
    }
}