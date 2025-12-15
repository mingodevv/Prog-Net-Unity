using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TankClass : HeroCore
{
    [Header("Tank - Attack Hitbox")]
    public MeshCollider attackCollider;
    public int attackDamage = 20;

    private HashSet<DummyPlayer> hitTargets = new HashSet<DummyPlayer>();

    public override void Skill_Attack()
    {
        if (!canAttack) return;
        StartCoroutine(AttackRoutine());
    }

    public override void Skill_Crouch()
    {

    }

    public override void Skill_PowerUp()
    {
        anim.SetTrigger("PowerUp?");
    }

    private IEnumerator AttackRoutine()
    {
        canAttack = false;
        anim.SetTrigger("Attack?");
        yield return new WaitForSeconds(attackCooldown);

        if (attackCollider != null)
            StartCoroutine(EnableAttackColliderRoutine());

        canAttack = true;
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