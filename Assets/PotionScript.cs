using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PotionScript : MonoBehaviour, ICollectible
{
    public static event Action OnPotionCollected;
    public Animator animator;
    public AudioManagerScript audioManager;
    public void Collect()
    {
        OnPotionCollected?.Invoke();
        animator.Play("potion_out");
        audioManager.PlaySFX(audioManager.collectSFX);
        Debug.Log("Potion Collected!");
        Destroy(gameObject);
    }
}
