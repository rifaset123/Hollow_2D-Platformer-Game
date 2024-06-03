using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovementScript : MonoBehaviour
{
    [Header("Movement")]
    public float direction = 0f;
    public float speed;
    public float jumpSpeed;
    public Rigidbody2D player;

    [Header("Switch")]
    public LineOfSight lineOfSight;
    public PlayerSwitcherScript switcher;

    [Header("Ground Check")]
    public bool isTouchingGround = false;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;


    [Header("Better Jump")]
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    [Header("Coyotte Time")]
    private float coyoteTime = 0.1f;
    public float coyoteCounter;

    [Header("Animator")]
    public Animator animator;
    public BoxCollider2D boxCollider2D;
    public TextManager txt;
    public ParticleSystem dust;

    [Header("SFX")]
    AudioManagerScript audioManager;


    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerScript>();
        boxCollider2D = boxCollider2D.GetComponent<BoxCollider2D>();
    }


    void Update()
    {
        // buat ngecek kalo player lagi di ground atau engga
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer) ;
        //normalJump();

        if (isTouchingGround)
        {
            coyoteCounter = coyoteTime;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }


        // controller PC
        // direction = Input.GetAxisRaw("Horizontal");
        PlayerAnimation();

        if (txt.isDialogueActive)
        {
            direction = 0f;
        }
    }

    private void FixedUpdate() // untuk physics
    {
        // move
        if (direction > 0f) // kanan
        {
            player.velocity = new Vector2(direction * speed, player.velocity.y);
            transform.localScale = new Vector2(2f,2f);
        }
        else if (direction < 0f)  // jalan kiri 
        {
            player.velocity = new Vector2(direction * speed, player.velocity.y);
            transform.localScale = new Vector2(-2f,2f);
        }
        else // pas diem
        {
            player.velocity = new Vector2(0, player.velocity.y);
        }

        jumpFallOfState();
    }

    void normalJump() // jump biasa
    {
        if (coyoteCounter > 0f)
        {
            audioManager.PlaySFX(audioManager.jumpSFX);
            player.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        }
    }

    public void jumpFallOfState() // jump dengan efek fall of state
    {
        if (player.velocity.y < 0)
        {
            player.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }   
        else if (player.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            player.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            coyoteCounter = 0f;
        }
    }
    void PlayerAnimation() // animasi
    {
        animator.SetFloat("Speed ", Mathf.Abs(player.velocity.x));
        animator.SetBool("OnGround", isTouchingGround);
        animator.SetFloat("yVelocity", player.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision) // kalo nyentuh tanah ada sfx
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            audioManager.PlaySFX(audioManager.dropSFX);
            dust.Play();
        }
    }

    // new input system (mobile)
    public void MobileMove(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<float>();
    }
    public void MobileJump(InputAction.CallbackContext context)
    {
        if (context.performed && coyoteCounter > 0f)
        {
            dust.Play();
            audioManager.PlaySFX(audioManager.jumpSFX);
            player.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        }
    }

    public void MobileSwitch(InputAction.CallbackContext context)
    {
        if (context.performed && lineOfSight.hasLineOfSight)
        {
            switcher.switchPlayer();
        }
    }
}

