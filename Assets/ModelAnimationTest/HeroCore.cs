using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CharacterController))]
public class HeroCore : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 3f;
    public float runSpeed = 6f;

    [Header("Health")]
    public int maxHealth = 200;
    protected int currentHealth;

    [Header("Attack")]
    public float attackCooldown = 0.8f;
    protected bool canAttack = true;

    protected CharacterController controller;
    protected Animator anim;

    protected virtual void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();

        currentHealth = maxHealth;
    }

    protected virtual void Update()
    {
        HandleMovement();
        HandleActions();
    }

    protected void HandleMovement()
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

    protected virtual void HandleActions()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && canAttack)
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        anim.SetTrigger("Attack?");
        StartCoroutine(AttackCooldownRoutine());
    }

    protected IEnumerator AttackCooldownRoutine()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    public virtual void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        currentHealth = Mathf.Max(0, currentHealth);

        Debug.Log($"{name} took {dmg} dmg. HP = {currentHealth}");

        if (currentHealth <= 0)
            Die();
    }

    protected virtual void Die()
    {
        Debug.Log($"{name} died!");
        gameObject.SetActive(false);
    }
}
