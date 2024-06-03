using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSwitcherScript : MonoBehaviour
{
    public PlayerMovementScript playerMovement;
    public PlayerInput playerInput;
    public List<SkeletonScript> skeletons; // List to hold multiple skeletons
    public List<PlayerInput> skeletonInputs;
    public LineOfSight lineOfSight;

    [Header("Sprite Control")]
    public SpriteRenderer playerSprite;
    public List<SpriteRenderer> skeletonSprites;
    public Material materialOutline;
    public Material defaultMaterial;


    public bool player1Active = true;
    public bool swap = false;

    [Header("SFX")]
    AudioManagerScript audioManager;

    private bool switching = false; // Flag to track whether a switch is in progress
    public float switchDelay = 0.5f; // Adjust this delay as needed
    public LayerMask obstacleLayerMask;


    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        swap = playerMovement.enabled;

        bool canSwitch = lineOfSight.hasLineOfSight;

        if (canSwitch && Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(SwitchingWithDelay());
        }
        else if (!canSwitch && !player1Active)
        {
            StartCoroutine(SwitchWithDelay());
        }
    }
    IEnumerator SwitchWithDelay()
    {
        switching = true; // Set flag to indicate switching is in progress

        // Perform the switch
        switchBack();

        // Wait for the specified delay
        yield return new WaitForSeconds(switchDelay);

        switching = false; // Reset the switching flag
    }
    IEnumerator SwitchingWithDelay()
    {
        switching = true; // Set flag to indicate switching is in progress

        // Perform the switch
        switchPlayer();

        // Wait for the specified delay
        yield return new WaitForSeconds(switchDelay);

        switching = false; // Reset the switching flag
    }

    public void switchPlayer()
    {
        SkeletonScript closestSkeleton = null;
        closestSkeleton = lineOfSight.detectedSkeleton;

        // Switch to the closest skeleton if found
        if (closestSkeleton != null && !switching)
        {
            if (!closestSkeleton.enabled)
            {
                // Disable player 1
                playerMovement.animator.SetFloat("Speed ", 0f);
                playerMovement.animator.SetBool("OnGround", true);
                playerMovement.animator.SetBool("isSwitch", true);
                playerSprite.material = defaultMaterial;
                player1Active = false;
                // colider
                playerMovement.boxCollider2D.offset = new Vector2(0.1065392f, -0.3705158f);
                playerMovement.boxCollider2D.size = new Vector2(0.7068434f, 0.2105141f);
                stopPlayer1();

                // Enable the chosen skeleton
                closestSkeleton.enabled = true;
                skeletons[skeletons.IndexOf(closestSkeleton)].animator.SetBool("isSwitch", false);
                skeletonInputs[skeletons.IndexOf(closestSkeleton)].enabled = true; // Enable input for this skeleton
                skeletonSprites[skeletons.IndexOf(closestSkeleton)].material = materialOutline; // Highlight the sprite
                closestSkeleton.boxCollider2D.offset = new Vector2(closestSkeleton.boxCollider2D.offset.x, -0.08337402f);
                closestSkeleton.boxCollider2D.size = new Vector2(closestSkeleton.boxCollider2D.size.x, 0.8518066f);

                audioManager.PlaySFX(audioManager.switchSFX);
            }
            else
            {
                // Disable the chosen skeleton
                closestSkeleton.enabled = false;
                skeletonInputs[skeletons.IndexOf(closestSkeleton)].enabled = false; // Disable input for this skeleton
                skeletonSprites[skeletons.IndexOf(closestSkeleton)].material = defaultMaterial; // Remove the highlight
                closestSkeleton.boxCollider2D.offset = new Vector2(closestSkeleton.boxCollider2D.offset.x, -0.2078857f);
                closestSkeleton.boxCollider2D.size = new Vector2(closestSkeleton.boxCollider2D.size.x, 0.6473389f);
                closestSkeleton.animator.SetBool("isSwitch", true);
                closestSkeleton.animator.SetBool("OnGround", true);

                // Enable player 1
                playerMovement.enabled = true;
                playerInput.enabled = true;
                player1Active = true;
                playerMovement.animator.SetBool("isSwitch", false);
                playerSprite.material = materialOutline;
                // colider
                playerMovement.boxCollider2D.offset = new Vector2(-0.03945291f, -0.08337402f);
                playerMovement.boxCollider2D.size = new Vector2(0.3964126f, 0.8518066f);

                audioManager.PlaySFX(audioManager.reverseSwitchSFX);
            }
        }
    }

    public void switchBack() // Switch back to player 1 perspective
    {
        player1Active = true;
        // Enable player 1
        playerMovement.enabled = true;
        playerInput.enabled = true;
        player1Active = true;
        playerMovement.animator.SetBool("isSwitch", false);

        playerSprite.material = materialOutline;
        // colider
        playerMovement.boxCollider2D.offset = new Vector2(-0.03945291f, -0.08337402f);
        playerMovement.boxCollider2D.size = new Vector2(0.3964126f, 0.8518066f);
        audioManager.PlaySFX(audioManager.reverseSwitchSFX);
        
        // Disable each skeleton
        foreach (var skeleton in skeletons)
        {
            skeleton.enabled = false;
            skeletonInputs[skeletons.IndexOf(skeleton)].enabled = false; // Disable input for this skeleton
            skeletonSprites[skeletons.IndexOf(skeleton)].material = defaultMaterial; // Remove the highlight
            skeletons[skeletons.IndexOf(skeleton)].boxCollider2D.offset = new Vector2(skeletons[skeletons.IndexOf(skeleton)].boxCollider2D.offset.x, -0.2078857f);
            skeletons[skeletons.IndexOf(skeleton)].boxCollider2D.size = new Vector2(skeletons[skeletons.IndexOf(skeleton)].boxCollider2D.size.x, 0.6473389f);
            skeletons[skeletons.IndexOf(skeleton)].animator.SetBool("isSwitch", true);
            skeletons[skeletons.IndexOf(skeleton)].animator.SetBool("OnGround", true);
        }
    }

    public void stopPlayer1()
    {
        playerMovement.enabled = false;
        playerInput.enabled = false;
    }
}