using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public void PlayGame(int levelID)
    {
        
        string levelName = "";

        // Map levelID to scene names
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

        SceneManager.LoadScene(levelName);
    }
    public Button[] buttons;

        private void Awake()
        {
            int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel",1);
            for (int i = 0; i < buttons.Length;i++){
                buttons[i].interactable = false;
            }
            for(int i = 0;i< unlockedLevel;i++){
                buttons[i].interactable = true;
            }
        }

    public void Exit()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
