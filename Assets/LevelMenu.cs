using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] levelButtons;

    private void Awake()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);       
        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = false;
        }
        for (int i = 0; i < unlockedLevel; i++)
        {
            levelButtons[i].interactable = true;
        }
    }
    public void OpenLevel(int levelId)
    {
        string levelName = "Stage " + levelId;
        SceneManager.LoadScene(levelName);
    }
}
