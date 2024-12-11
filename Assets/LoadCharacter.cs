using UnityEngine;

public class LoadSelectedCharacter : MonoBehaviour
{
    public GameObject[] characterPrefabs; 
    private string selectedCharacterKey = "SelectedCharacter";

    void Start()
    {
        
        int selectedCharacterIndex = PlayerPrefs.GetInt(selectedCharacterKey, 0);

        if (selectedCharacterIndex >= 0 && selectedCharacterIndex < characterPrefabs.Length)
        {
            Instantiate(characterPrefabs[selectedCharacterIndex], Vector3.zero, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Invalid character index loaded from PlayerPrefs!");
        }
    }
}