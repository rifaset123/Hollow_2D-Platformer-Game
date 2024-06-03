using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionMenuScript : MonoBehaviour
{
    public GameObject particle;
    public bool isParticleOn = true;

    public void RestartLevel()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
       Time.timeScale = 1;
    }
    public void MainMenuLevel()
    {
       SceneManager.LoadSceneAsync(0);
       Time.timeScale = 1;
    }

    public void ParticleOff()
    {
        if (isParticleOn)
        {
            particle.SetActive(false);
            isParticleOn = false;
        }
        else
        {
            particle.SetActive(true);
            isParticleOn = true;
        }
    }

}
