using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTrigger : MonoBehaviour
{
    public TextDialogue dialogue;
    public BoxCollider2D boxCollider2D;

    public void TriggerDialogue()
    {
        FindObjectOfType<TextManager>().StartDialogue(dialogue);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Skeleton")
        {
            TriggerDialogue();
        }
        if (boxCollider2D.enabled)
        {
            boxCollider2D.enabled = false;
        }
    }
}
