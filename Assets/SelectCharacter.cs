using UnityEngine;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour
{
    public GameObject[] characters; // Array ของตัวละครในรูปแบบ GameObject
    public Button[] characterButtons; // Array ของปุ่ม UI สำหรับเลือกตัวละคร
    private int selectedCharacterIndex = -1; // ใช้แทน PlayerPrefs

    void Start()
    {
        if (characters == null || characters.Length < 2)
        {
            Debug.LogError("Please assign at least 2 characters in the array.");
            return;
        }

        if (characterButtons == null || characterButtons.Length != characters.Length)
        {
            Debug.LogError("Please assign the same number of buttons as characters.");
            return;
        }

        // Debugging: แสดงรายการตัวละครทั้งหมด
        for (int i = 0; i < characters.Length; i++)
        {
            Debug.Log($"Character {i}: {characters[i]?.name}");
            int index = i; // Capturing the current value of i
            characterButtons[i].onClick.AddListener(() => SelectCharacter(index));
        }

        // กำหนดให้ตัวละครทั้งหมด "ปิดการใช้งาน" ยกเว้นตัวแรก
        foreach (GameObject character in characters)
        {
            if (character != null)
            {
                character.SetActive(true);
            }
        }
    }

    public void SelectCharacter(int characterIndex)
    {
        if (characterIndex >= 0 && characterIndex < characters.Length)
        {

        selectedCharacterIndex = characterIndex; // บันทึก index ที่เลือกไว้
        Debug.Log($"Selected character: {characters[characterIndex].name}");
        }
        else
        {
        Debug.LogError("Invalid character index!");
        }
    }

    public GameObject GetSelectedCharacter()
    {
        if (selectedCharacterIndex >= 0 && selectedCharacterIndex < characters.Length)
        {
            return characters[selectedCharacterIndex];
        }

        return null; // หากไม่มีตัวละครที่เลือก
    }
}