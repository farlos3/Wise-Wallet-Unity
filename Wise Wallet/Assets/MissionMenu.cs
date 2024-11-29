using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MissionMenu : MonoBehaviour
{
   public void OpenLevel(int levelID)
   {
    string levelName = "Level" + levelID;
    SceneManager.LoadScene(levelName);
   }
}
