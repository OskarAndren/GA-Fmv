using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator ani;
    BoxCollider2D feet_Col;
    CapsuleCollider2D body_Col;

    Vector2 moveInput;
    [SerializeField] Vector2 deathKick;

    public bool isAlive = true;

    public float moveSpeed = 5f;
    public float jumpSpeed = 10f;
    private float maxYVeolcity = -20f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        feet_Col = GetComponent<BoxCollider2D>();
        body_Col = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        if (rb.velocity.y < maxYVeolcity) rb.velocity = new Vector2(rb.velocity.x, maxYVeolcity);
        if (isAlive)
            Run();
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
        if (body_Col.IsTouchingLayers(LayerMask.GetMask("Enemies")))
        {
            isAlive = false;
            ani.SetBool("isDead", true);
            rb.velocity = deathKick;
            feet_Col.enabled = false;
            body_Col.enabled = false;
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (feet_Col.IsTouchingLayers(LayerMask.GetMask("Enemies")))
        {
            Destroy(other.gameObject);
            rb.velocity += new Vector2(rb.velocity.x, 20);
        }
    }
}
