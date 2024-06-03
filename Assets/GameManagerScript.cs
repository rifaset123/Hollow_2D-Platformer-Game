using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject optionMenu;
    public GameObject mobileControlUI;

    [Header("SFX")]
    AudioManagerScript audioManager;
    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerScript>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }
    public void PauseGame()
    {
        if (optionMenu.activeSelf)
        {
            optionMenu.SetActive(false);
            mobileControlUI.SetActive(true);
            Time.timeScale = 1;
        }
        else
        {
            optionMenu.SetActive(true);
            mobileControlUI.SetActive(false);
            Time.timeScale = 0;
        }
    }

    public void PlayTutorialSoundIn()
    {
        audioManager.PlaySFX(audioManager.openDialogueSFX);
    }    
    public void PlayClickSound()
    {
        audioManager.PlaySFX(audioManager.clickDialogueSFX);
    }
}
