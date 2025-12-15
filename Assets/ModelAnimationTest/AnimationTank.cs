using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class TankClass : HeroCore
{
    [Header("Tank - Attack Hitbox")]
    public MeshCollider attackCollider;
    public int attackDamage = 20;

    private HashSet<DummyPlayer> hitTargets = new HashSet<DummyPlayer>();

    protected override void Start()
    {
        base.Start();

        if (attackCollider != null)
            attackCollider.enabled = false;
    }

    protected override void HandleActions()
    {
        bool shielding = Mouse.current.rightButton.isPressed;
        anim.SetBool("Shield?", shielding);
      
        if (shielding) return;
        
        if (Mouse.current.leftButton.wasPressedThisFrame && canAttack)
        {
            Attack();
        }
        
        if (Keyboard.current.qKey.wasPressedThisFrame)
            anim.SetTrigger("PowerUp?");
    }

    protected override void Attack()
    {
        anim.SetTrigger("Attack?");
        StartCoroutine(AttackCooldownRoutine());

        if (attackCollider != null)
            StartCoroutine(EnableAttackColliderRoutine());
    }

    private IEnumerator EnableAttackColliderRoutine()
    {
        hitTargets.Clear();
        attackCollider.enabled = true;
        
        yield return new WaitForSeconds(0.5f);

        attackCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (attackCollider == null || !attackCollider.enabled) return;

        DummyPlayer target = other.GetComponent<DummyPlayer>();

        if (target != null && !hitTargets.Contains(target))
        {
            target.TakeDamage(attackDamage);
            hitTargets.Add(target);
        }
    }
    
    public override void TakeDamage(int dmg)
    {
        if (anim.GetBool("Shield?"))
        {
            Debug.Log($"{name} blocked the damage!");
            return;
        }

        base.TakeDamage(dmg);
    }
}
