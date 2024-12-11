using UnityEngine;

public class LoadSelectedCharacter : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    private string selectedCharacterKey = "SelectedCharacter";
    private static GameObject activeCharacter;
    public float groundLevelY = -2.3f; // ระดับพื้นด้านล่างในแกน Y
    public float spawnDepth = 3f; // ความลึกของตำแหน่ง Z ให้ตัวละครอยู่ข้างหน้า
    public int sortingOrder = 2; // Order in Layer ของ Sprite Renderer
    public float spawnPositionX = -7.5f; // ตำแหน่งแกน X ให้ตัวละครอยู่ทางซ้าย
    public Vector3 characterScale = new Vector3(0.8f, 0.8f, 0.8f); // ขนาดตัวละคร (ค่าเริ่มต้นคือ 1x1x1)

    void Awake()
    {
        if (activeCharacter == null)
        {
            int selectedCharacterIndex = PlayerPrefs.GetInt(selectedCharacterKey, 0);

            if (selectedCharacterIndex >= 0 && selectedCharacterIndex < characterPrefabs.Length)
            {
                Vector3 spawnPosition = new Vector3(spawnPositionX, groundLevelY, spawnDepth); // ปรับตำแหน่งแกน X ไปทางซ้าย
                activeCharacter = Instantiate(characterPrefabs[selectedCharacterIndex], spawnPosition, Quaternion.identity);
                activeCharacter.transform.localScale = characterScale; // ปรับขนาดตัวละคร
                DontDestroyOnLoad(activeCharacter);
            }
            else
            {
                Debug.LogError("Invalid character index loaded from PlayerPrefs!");
            }
        }
        else
        {
            Vector3 spawnPosition = new Vector3(spawnPositionX, groundLevelY, spawnDepth); // ปรับตำแหน่งแกน X ไปทางซ้าย
            activeCharacter.transform.position = spawnPosition;
        }

        // ตรวจสอบและตั้งค่า Order in Layer ของ Sprite Renderer
        SpriteRenderer spriteRenderer = activeCharacter.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = sortingOrder;
        }
    }
}
