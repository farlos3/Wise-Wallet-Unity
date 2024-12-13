using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelector : MonoBehaviour
{
    public GameObject[] characters;
    public Button[] characterButtons;
    private string selectedCharacterKey = "SelectedCharacter";

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

        for (int i = 0; i < characters.Length; i++)
        {
            int index = i;
            characterButtons[i].onClick.AddListener(() => SelectCharacter(index));
        }
    }

    public void SelectCharacter(int characterIndex)
    {
        if (characterIndex >= 0 && characterIndex < characters.Length)
        {
            PlayerPrefs.SetInt(selectedCharacterKey, characterIndex);
            PlayerPrefs.Save();
            Debug.Log($"Selected character: {characters[characterIndex].name}");

            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            Debug.LogError("Invalid character index!");
        }
    }
}