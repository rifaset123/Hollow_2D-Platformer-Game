using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectibleManager : MonoBehaviour
{
    public TextMeshProUGUI potionUI;
    int potionCollected = 0;

    private void OnEnable()
    {
        PotionScript.OnPotionCollected += PotionCollected;
    }
    private void OnDisable()
    {
        PotionScript.OnPotionCollected -= PotionCollected;
    }
    private void PotionCollected()
    {
        potionCollected++;
        potionUI.text = potionCollected.ToString();
    }
}
