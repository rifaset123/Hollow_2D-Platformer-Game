using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMCamera : MonoBehaviour
{
    public PlayerSwitcherScript playerSwitch;
    public CinemachineVirtualCamera vcam;

    [Header("SFX")]
    AudioManagerScript audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerScript>();
    }
    void Update()
    {
        if (playerSwitch.player1Active)
        {
            // If player 1 is active, follow the player
            vcam.Follow = playerSwitch.playerMovement.transform;
        }
        else
        {
            // If a skeleton is active, find the active skeleton and follow it
            foreach (var skeleton in playerSwitch.skeletons)
            {
                if (skeleton.enabled)
                {
                    vcam.Follow = skeleton.transform;
                    break; // Once the active skeleton is found, exit the loop
                }
            }
        }
    }
}
