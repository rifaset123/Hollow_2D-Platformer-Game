using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class TextManager : MonoBehaviour
{

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public bool isDialogueActive = false;
    public Button btnNext;
    public GameObject mobileControl;
    public PlayerMovementScript playerMovement;
    public SkeletonScript skeletonMovement;

    public Animator animator;

    public Queue<string> sentences; // Queue = FIFO (First In First Out)

    [Header("SFX")]
    AudioManagerScript audioManager;


    void Start()
    {
        sentences = new Queue<string>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerScript>();
    }

    public void StartDialogue(TextDialogue dialogue)
    {
        mobileControl.SetActive(false);
        playerMovement.enabled = false;
        skeletonMovement.enabled = false;
        playerMovement.animator.SetFloat("Speed ", 0f);
        playerMovement.animator.SetBool("OnGround", true);
        audioManager.PlaySFX(audioManager.openDialogueSFX);
        animator.SetBool("Play", true);
        Debug.Log("Starting conversation with " + dialogue.name);

        nameText.text = dialogue.name; // nampilin nama karakter

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        
        isDialogueActive = true;
        btnNext.enabled = true;
        Debug.Log("Displaying Next Sentence");
        audioManager.PlaySFX(audioManager.clickDialogueSFX);

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines(); // nge stop kalimat yang lagi jalan
        StartCoroutine(TypeSentence(sentence)); // nampilin kalimat per huruf

    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.01f); // Adjust delay time as needed
        }
    }


    void EndDialogue()
    {
        mobileControl.SetActive(true);
        playerMovement.enabled = true;
        skeletonMovement.enabled = true;
        isDialogueActive = false;
        btnNext.enabled = false;
        animator.SetBool("Play", false);
        audioManager.PlaySFX(audioManager.openDialogueSFX);
        Debug.Log("End of conversation");
    }
}
