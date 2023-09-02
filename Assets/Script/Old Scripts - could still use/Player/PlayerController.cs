using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    // Movement settings
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintMultiplier = 1.5f;

    // Jump settings
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;

    // Stamina settings
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float staminaRegenRate = 5f;
    [SerializeField] private float sprintStaminaCost = 10f;
    [SerializeField] private float attackStaminaCost = 10f;
    [SerializeField] private float blockStaminaCost = 5f;

    // Stats
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float maxMana = 100f;
    //[SerializeField] private float strength = 10f;
    //[SerializeField] private float dexterity = 10f;

    private float currentHealth;
    private float currentMana;
    private float currentStamina;

    private bool isSprinting = false;
    private bool isGrounded = false;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public float HorizontalInput { get; private set; }
    public bool IsSprinting { get; private set; }
    public bool IsWalking { get; private set; }
    public bool IsGrounded { get; private set; }
    public bool IsBlocking { get; private set; }
    public bool IsAttacking { get; private set; }

    // Change the type of inventoryUI to GameObject
    public GameObject inventoryUI;
    // Add a new public variable for the InventoryUI component
    public GameObject inventoryUIObject;

    public CharacterModel characterModel;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        currentHealth = maxHealth;
        currentMana = maxMana;
        currentStamina = maxStamina;
    }

    private void Update()
    {
        HandleMovement();
        HandleJump();
        // Check if inventoryUI is not active before handling attacks
        if (!inventoryUI.activeInHierarchy)
        {
            HandleAttack();
            HandleRangedAttack();
        }
        HandleBlock();
        RegenerateStamina();
    }


    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        isSprinting = Input.GetKey(KeyCode.LeftShift) && currentStamina > 0;
        float speed = moveSpeed * (isSprinting ? sprintMultiplier : 1);

        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

        if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        if (horizontalInput != 0)
        {
            animator.SetBool("isWalking", true);
            if (isSprinting)
            {
                animator.SetFloat("walkSpeedMultiplier", sprintMultiplier);
            }
            else
            {
                animator.SetFloat("walkSpeedMultiplier", 1);
            }
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if (isSprinting && Mathf.Abs(horizontalInput) > 0)
        {
            currentStamina -= sprintStaminaCost * Time.deltaTime;
        }
    }

    private void HandleJump()
    {
        isGrounded = Physics2D.OverlapCircle(transform.position, 0.1f, groundLayer);

        if (isGrounded)
        {
            animator.SetBool("isJumping", false);
        }
        else
        {
            animator.SetBool("isJumping", true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }


    private void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0) && currentStamina >= attackStaminaCost)
        {
            IsAttacking = true;
            animator.SetTrigger("attack");
            currentStamina -= attackStaminaCost;
        }
        else
        {
            IsAttacking = false;
        }
    }

    private void HandleRangedAttack()
    {
        if (Input.GetMouseButtonDown(1) && currentStamina >= attackStaminaCost)
        {
            GetComponentInChildren<RangedAttack>().FireProjectile();
            animator.SetTrigger("ranged");
            currentStamina -= attackStaminaCost;
        }
    }

    private void HandleBlock()
    {
        if (Input.GetKey(KeyCode.S) && currentStamina >= blockStaminaCost)
        {
            IsBlocking = true;
            animator.SetBool("block", true); // Set "block" parameter to true
            currentStamina -= blockStaminaCost * Time.deltaTime; // Consume stamina gradually while blocking
        }
        else
        {
            IsBlocking = false;
            animator.SetBool("block", false); // Set "block" parameter to false
        }
    }


    private void RegenerateStamina()
    {
        if (currentStamina < maxStamina)
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            if (currentStamina > maxStamina)
            {
                currentStamina = maxStamina;
            }
        }
    }

    // IDamageable interface implementation
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Handle player death logic here (e.g., play an animation, respawn, etc.)
    }

    public void ResetTrigger(string triggerName)
    {
        animator.ResetTrigger(triggerName);
    }

}


