using UnityEngine;

public class LoadSelectedCharacter : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    private string selectedCharacterKey = "SelectedCharacter";
    private static GameObject activeCharacter;

    void Awake()
    {
        if (activeCharacter == null)
        {
            int selectedCharacterIndex = PlayerPrefs.GetInt(selectedCharacterKey, 0);

            if (selectedCharacterIndex >= 0 && selectedCharacterIndex < characterPrefabs.Length)
            {
                activeCharacter = Instantiate(characterPrefabs[selectedCharacterIndex], Vector3.zero, Quaternion.identity);
                DontDestroyOnLoad(activeCharacter);
            }
            else
            {
                Debug.LogError("Invalid character index loaded from PlayerPrefs!");
            }
        }
        else
        {
            activeCharacter.SetActive(true);
            DontDestroyOnLoad(activeCharacter);
        }
    }

    void OnDestroy()
    {
        if (activeCharacter != null)
        {
            activeCharacter.SetActive(false); // ทำให้ตัวละครไม่ถูกทำลายเมื่อ Scene นี้ถูกทำลาย
        }
    }
}