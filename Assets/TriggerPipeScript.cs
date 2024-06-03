using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPipeScript : MonoBehaviour
{
    [SerializeField] public Animator triggerAnim = null;
    [SerializeField] public Animator gateAnim = null;

    [SerializeField] AudioManagerScript audioManager;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Skeleton"))
        {
            triggerAnim.Play("pipe_in");
            gateAnim.Play("gate_in");
            audioManager.PlaySFX(audioManager.gateInSFX);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Skeleton"))
        {
            triggerAnim.Play("pipe_out");
            gateAnim.Play("gate_out");
            audioManager.PlaySFX(audioManager.gateOutSFX);
        }
    }
}
