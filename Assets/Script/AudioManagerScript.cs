using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("Audio Clip")]
    public AudioClip background;
    public AudioClip bgm1;
    public AudioClip bgm2;
    public AudioClip bgm3;
    public AudioClip bgm4;
    public AudioClip bgm5;
    public AudioClip mainMenuSFX;
    public AudioClip jumpSFX;
    public AudioClip dropSFX;
    public AudioClip switchSFX;
    public AudioClip reverseSwitchSFX;
    public AudioClip clickDialogueSFX;
    public AudioClip openDialogueSFX;
    public AudioClip gateInSFX;
    public AudioClip gateOutSFX;
    public AudioClip collectSFX;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {

        sfxSource.PlayOneShot(clip);
    }
}
