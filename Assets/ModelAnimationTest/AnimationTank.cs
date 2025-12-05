using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")] public float walkSpeed = 3f;
    public float runSpeed = 6f;

    [Header("Health")] public int maxHealth = 200;
    private int currentHealth;

    [Header("Attack")] public MeshCollider attackCollider; 
    public int attackDamage = 20;
    public float attackCooldown = 0.8f;
    private float attackTimer = 0f;

    private CharacterController controller;
    private Animator anim;
    
    private HashSet<DummyPlayer> hitTargets = new HashSet<DummyPlayer>();

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();

        currentHealth = maxHealth;

        if (attackCollider != null)
            attackCollider.enabled = false;
    }

    void Update()
    {
        HandleMovement();
        HandleActions();
        UpdateAttackCooldown();
    }

    void HandleMovement()
    {
        if (Keyboard.current == null) return;

        Vector2 move = new Vector2(
            (Keyboard.current.dKey.isPressed ? 1 : 0) - (Keyboard.current.aKey.isPressed ? 1 : 0),
            (Keyboard.current.wKey.isPressed ? 1 : 0) - (Keyboard.current.sKey.isPressed ? 1 : 0)
        );

        Vector3 direction = new Vector3(move.x, 0, move.y).normalized;

        float targetBlend = 0f;

        if (direction.sqrMagnitude > 0.1f)
        {
            bool isRunning = Keyboard.current.leftShiftKey.isPressed;

            targetBlend = isRunning ? 1f : 0.5f;
            float speed = isRunning ? runSpeed : walkSpeed;

            controller.Move(direction * speed * Time.deltaTime);

            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.LookRotation(direction),
                10f * Time.deltaTime
            );
        }

        anim.SetFloat("Speed", targetBlend);
    }

    void HandleActions()
    {
        anim.SetBool("Shield?", Mouse.current.rightButton.isPressed);

        if (anim.GetBool("Shield?")) return;

        if (attackTimer <= 0f && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Attack();
        }

        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            anim.SetTrigger("PowerUp?");
        }
    }

    private void Attack()
    {
        anim.SetTrigger("Attack?");
        attackTimer = attackCooldown;

        if (attackCollider != null)
            StartCoroutine(EnableAttackCollider());
    }

    private IEnumerator EnableAttackCollider()
    {
        hitTargets.Clear(); 
        attackCollider.enabled = true;
        yield return new WaitForSeconds(0.5f);
        attackCollider.enabled = false;
    }

    private void UpdateAttackCooldown()
    {
        if (attackTimer > 0f)
            attackTimer -= Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        if (anim.GetBool("Shield?"))
        {
            Debug.Log($"{gameObject.name} a bloqué les dégâts avec le bouclier !");
            return;
        }

        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        Debug.Log($"Player {gameObject.name} took {damage} damage. HP: {currentHealth}");

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        Debug.Log($"Player {gameObject.name} died!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (attackCollider != null && attackCollider.enabled)
        {
            DummyPlayer target = other.GetComponent<DummyPlayer>();
            if (target != null && !hitTargets.Contains(target))
            {
                target.TakeDamage(attackDamage);
                hitTargets.Add(target);
            }
        }
    }
}
