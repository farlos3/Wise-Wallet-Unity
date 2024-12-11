using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    public GameObject[] characters;
    private string selectedCharacterKey = "SelectedCharacter";

    void Start()
    {
        
        if (characters == null || characters.Length < 2)
        {
            Debug.LogError("Please assign at least 2 characters in the array.");
            return;
        }

        for (int i = 0; i < characters.Length; i++)
        {
        Debug.Log($"Character {i}: {characters[i]?.name}");
        }

        
        foreach (GameObject character in characters)
        {
            if (character != null)
            {
                character.SetActive(true); 
            }
        }
    }

    
    public void SelectCharacter1()
    {
        SaveSelectedCharacter(0); 
    }

    
    public void SelectCharacter2()
    {
        SaveSelectedCharacter(1);
    }

    private void SaveSelectedCharacter(int characterIndex)
    {
        if (characterIndex >= 0 && characterIndex < characters.Length)
        {
            PlayerPrefs.SetInt(selectedCharacterKey, characterIndex);
            Debug.Log($"Selected character index saved: {characterIndex}");
        }
        else
        {
            Debug.LogError("Invalid character index!");
        }
    }
}