using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpSpeed = 20f;
    [SerializeField] int climbSpeed = 7;
    [SerializeField] Vector2 deathKick;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] GameOverScript gameOverScript;
    [SerializeField] TextMeshProUGUI coinsText;
    [SerializeField] TextMeshProUGUI levelText;

    Rigidbody2D rb;
    Animator ani;
    Collider2D bodyColl;
    Collider2D feetColl;
    Vector2 moveInput;

    bool isClimbing = false;
    float startGravityScale;

    public int coinsPickedUp;
    int totalCoinsInScene;
    int currentSceneIndex;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        bodyColl = GetComponent<CapsuleCollider2D>();
        feetColl = GetComponent<BoxCollider2D>();
        startGravityScale = rb.gravityScale;

        Scene currentScene = SceneManager.GetActiveScene();
        currentSceneIndex = currentScene.buildIndex;
        if (currentSceneIndex == 0)
            totalCoinsInScene = 6;
        else if (currentSceneIndex == 1)
            totalCoinsInScene = 3;
        else if (currentSceneIndex == 2)
            totalCoinsInScene = 4;

        coinsText.text = "Coins: " + coinsPickedUp + "/" + totalCoinsInScene;
        levelText.text = "Level: " + (currentSceneIndex + 1) + "/3";
    }

    void Update()
    {
        if (Globals.isAlive && !Globals.gameWon)
        {
            Run();
            Climb();
            if (coinsPickedUp == totalCoinsInScene && currentSceneIndex < 2)
                SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else if (!Globals.isAlive)
        {
            gameOverScript.SetUp();
        }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump()
    {
        if (feetColl.IsTouchingLayers(LayerMask.GetMask("Ground")) || feetColl.IsTouchingLayers(LayerMask.GetMask("Moving Platform")))
        {
            rb.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void OnFire()
    {
        float rotateZ = 0f;
        if (transform.localScale.x < 0)
        {
            rotateZ = 180f;
        }
        GameObject newBullet = Instantiate(bullet, bulletSpawnPoint.position, Quaternion.Euler(0, 0, rotateZ));

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

    void Climb()
    {
        if (bodyColl.IsTouchingLayers(LayerMask.GetMask("Climb")))
        {
            if (moveInput.y != 0)
            {
                isClimbing = true;
                ani.speed = 1;
            }
            else
            {
                ani.speed = 0;
            }
            if (isClimbing)
            {
                rb.gravityScale = 0;
                Vector2 climbVelocity = new Vector2(rb.velocity.x, moveInput.y * climbSpeed);
                rb.velocity = climbVelocity;

                ani.SetBool("isClimbing", true);
            }
        }
        else
        {
            rb.gravityScale = startGravityScale;
            ani.speed = 1;
            ani.SetBool("isClimbing", false);
            isClimbing = false;
        }

    }

    void OnCollisionEnter2D(Collision2D other) //Enemie death/kill. And Spike death
    {
        if (bodyColl.IsTouchingLayers(LayerMask.GetMask("Enemies")) || bodyColl.IsTouchingLayers(LayerMask.GetMask("Water")) || feetColl.IsTouchingLayers(LayerMask.GetMask("Water")) || bodyColl.IsTouchingLayers(LayerMask.GetMask("Bird")) || feetColl.IsTouchingLayers(LayerMask.GetMask("Bird")))
        {
            Globals.isAlive = false;
            ani.SetTrigger("Death");
            rb.velocity = deathKick;
            bodyColl.enabled = false;
            feetColl.enabled = false;
        }
        else if (feetColl.IsTouchingLayers(LayerMask.GetMask("Enemies")))
        {
            Destroy(other.gameObject);
        }

        if (bodyColl.IsTouchingLayers(LayerMask.GetMask("Spikes")) || feetColl.IsTouchingLayers(LayerMask.GetMask("Spikes")) || bodyColl.IsTouchingLayers(LayerMask.GetMask("Bullet")))
        {
            Globals.isAlive = false;
            ani.SetTrigger("Death");
            rb.velocity = deathKick;
            bodyColl.enabled = false;
            feetColl.enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other) //coins
    {
        if (bodyColl.IsTouchingLayers(LayerMask.GetMask("Coin")))
        {
            coinsPickedUp++;
            coinsText.text = "Coins: " + coinsPickedUp + "/" + totalCoinsInScene;
            Destroy(other.gameObject);
        }
    }
}
