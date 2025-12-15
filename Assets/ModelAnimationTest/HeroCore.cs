using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public abstract class HeroCore : Character
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

    protected virtual void Awake()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();

        currentHealth = maxHealth;
    }

    public void Move(Vector2 input, bool isSprinting)
    {
        Vector3 direction = new Vector3(input.x, 0, input.y).normalized;
        if (direction.sqrMagnitude < 0.01f)
        {
            anim.SetFloat("Speed", 0f);
            return;
        }

        float speed = isSprinting ? runSpeed : walkSpeed;
        controller.Move(direction * speed * Time.deltaTime);

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            Quaternion.LookRotation(direction),
            10f * Time.deltaTime
        );

        anim.SetFloat("Speed", isSprinting ? 1f : 0.5f);
    }

    public virtual void Attack()
    {
        if (!canAttack) return;

        anim.SetTrigger("Attack?");
        StartCoroutine(AttackCooldownRoutine());
    }

    protected IEnumerator AttackCooldownRoutine()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    public override void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        currentHealth = Mathf.Max(0, currentHealth);

        if (currentHealth <= 0)
            Die();
    }

    protected virtual void Die()
    {
        gameObject.SetActive(false);
    }

    public abstract void Skill_Crouch();
    public abstract void Skill_PowerUp();
}