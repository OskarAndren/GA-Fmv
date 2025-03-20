using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator ani;
    BoxCollider2D feet_Col;
    CapsuleCollider2D body_Col;

    [SerializeField] TextMeshProUGUI coinsText;
    [SerializeField] GameOverScreenShowcase GameOverScreenShowcase;
    [SerializeField] WinScreenShowcase WinScreenShowcase;
    [SerializeField] HealthBarScript healthBar;

    Vector2 moveInput;
    [SerializeField] Vector2 deathKick;

    public bool isAlive = true;

    public float moveSpeed = 5f;
    public float jumpSpeed = 10f;
    private float maxYVeolcity = -20f;

    public int coinsPickedUp;
    public int playerHealth;
    public int maxPlayerHealth = 3;
    int totalCoinsInScene = 5;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        feet_Col = GetComponent<BoxCollider2D>();
        body_Col = GetComponent<CapsuleCollider2D>();

        coinsText.text = "Coins: " + coinsPickedUp + "/" + totalCoinsInScene;

        playerHealth = maxPlayerHealth;
    }

    void Update()
    {
        if (rb.velocity.y < maxYVeolcity) rb.velocity = new Vector2(rb.velocity.x, maxYVeolcity);
        if (isAlive)
        {
            if (coinsPickedUp == totalCoinsInScene)
            {
                WinScreenShowcase.SetUp();
                rb.velocity = Vector2.zero;
            }
            else Run();
            if (playerHealth <= 0) isAlive = false;
        }
        else
        {
            ani.SetBool("isDead", true);
            rb.velocity = deathKick;
            feet_Col.enabled = false;
            body_Col.enabled = false;
            GameOverScreenShowcase.SetUp();
        }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    void OnJump()
    {
        if (feet_Col.IsTouchingLayers(LayerMask.GetMask("Ground")))
            rb.velocity += new Vector2(0f, jumpSpeed);
    }
    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;

        if (moveInput.x != 0)
        {
            ani.SetBool("isRunning", true);
            transform.localScale = new Vector2(Mathf.Sign(moveInput.x), transform.localScale.y);
        }
        else
        {
            ani.SetBool("isRunning", false);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        CapsuleCollider2D enemyCapsuleCollider = other.collider.GetComponent<CapsuleCollider2D>();

        if (body_Col.IsTouchingLayers(LayerMask.GetMask("Enemies")) && (enemyCapsuleCollider != null && body_Col.IsTouching(enemyCapsuleCollider)))
        {
            playerHealth--;
            healthBar.SetHealth(playerHealth);
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (feet_Col.IsTouchingLayers(LayerMask.GetMask("Enemies")))
        {
            Destroy(other.gameObject);
            rb.velocity += new Vector2(rb.velocity.x, 20);
        }

        if (body_Col.IsTouchingLayers(LayerMask.GetMask("Coin")))
        {
            coinsPickedUp++;
            coinsText.text = "Coins: " + coinsPickedUp + "/" + totalCoinsInScene;
            Destroy(other.gameObject);
        }
    }
}
