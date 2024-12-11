using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button[] buttons; // ปุ่มที่ใช้สำหรับเลือกด่าน

    private void Awake()
    {
        // ดึงข้อมูลระดับที่ปลดล็อกจาก PlayerPrefs
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        // ปิดการทำงานของปุ่มทั้งหมด
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        // เปิดใช้งานปุ่มตามระดับที่ปลดล็อก
        for (int i = 0; i < unlockedLevel; i++)
        {
            if (i < buttons.Length)
            {
                buttons[i].interactable = true;
            }
        }
    }

    public void PlayGame(int levelID)
    {
        string levelName = "";

        // แมป levelID ไปยังชื่อ Scene
        switch (levelID)
        {
            case 1:
                levelName = "Market Map";
                break;
            case 2:
                levelName = "School";
                break;
            case 3:
                levelName = "ShoppinMall";
                break;
            case 4:
                levelName = "Trade";
                break;
            default:
                Debug.LogError("Invalid levelID provided!");
                return;
        }

        // โหลด Scene แบบ Asynchronous
        StartCoroutine(LoadSceneAsync(levelName));
    }

    private IEnumerator LoadSceneAsync(string levelName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelName);

        // แสดง Progress (เพิ่มระบบ UI เช่น Loading Bar ถ้าต้องการ)
        while (!operation.isDone)
        {
            Debug.Log($"Loading progress: {operation.progress * 100}%");
            yield return null;
        }
    }

    public void Exit()
    {
        // โหลด Scene เริ่มต้น (เช่น Main Menu)
        SceneManager.LoadSceneAsync(0);
    }
}