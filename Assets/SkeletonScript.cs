using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkeletonScript : MonoBehaviour
{
    [Header("Movement")]
    public float direction = 0f;
    public float speed;
    public float jumpSpeed;
    public Rigidbody2D skeleton;

    [Header("Ground Check")]
    public bool isTouchingGround = false;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;

    [Header("Switch")]
    public LineOfSight lineOfSight;
    public PlayerSwitcherScript switcher;


    [Header("Better Jump")]
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    [Header("Coyotte Time")]
    private float coyoteTime = 0.2f;
    public float coyoteCounter;
    public ParticleSystem dust;

    [Header("Animator")]
    public Animator animator;
    public BoxCollider2D boxCollider2D;
    public Rigidbody2D rb2D;

    [Header("SFX")]
    AudioManagerScript audioManager;

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerScript>();
        boxCollider2D = boxCollider2D.GetComponent<BoxCollider2D>();
        rb2D = rb2D.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        //normalJump();
        jumpFallOfState();

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
        SkeletonAnimation();
    }

    private void FixedUpdate() // untuk physics
    {
        // move
        if (direction > 0f) // kanan
        {
            skeleton.velocity = new Vector2(direction * speed, skeleton.velocity.y);
            transform.localScale = new Vector2(2f, 2f);
        }
        else if (direction < 0f)  // jalan kiri
        {
            skeleton.velocity = new Vector2(direction * speed, skeleton.velocity.y);
            transform.localScale = new Vector2(-2f, 2f);
        }
        else // pas diem
        {
            skeleton.velocity = new Vector2(0, skeleton.velocity.y);
        }
    }

    void normalJump()
    {
        if (Input.GetButtonDown("Jump") && coyoteCounter > 0f)
        {
            audioManager.PlaySFX(audioManager.jumpSFX);
            skeleton.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        }
    }
    void JumpWOCoyotte()
    {
        if (Input.GetButtonDown("Jump"))
        {
            audioManager.PlaySFX(audioManager.jumpSFX);
            skeleton.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        }
    }

    void jumpFallOfState()
    {
        if (skeleton.velocity.y < 0)
        {
            skeleton.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (skeleton.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            skeleton.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            coyoteCounter = 0f;
        }
    }
    void SkeletonAnimation()
    {
        animator.SetFloat("Speed ", Mathf.Abs(skeleton.velocity.x));
        animator.SetBool("OnGround", isTouchingGround);
        animator.SetFloat("yVelocity", Mathf.Abs(skeleton.velocity.y));
    }
    // new input system (mobile)
    public void MobileMove(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<float>();
        Debug.Log("MOVED");
    }
    public void MobileJump(InputAction.CallbackContext context)
    {
        if (context.performed && coyoteCounter > 0f)
        {
            dust.Play();
            audioManager.PlaySFX(audioManager.jumpSFX);
            skeleton.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            // jumpFallOfState();
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
