using UnityEngine;
using System.Collections;

public class SniperClass : HeroCore
{
    [Header("Sniper Settings")]
    public GameObject bulletPrefab; 
    public Transform firePoint;
    public float bulletSpeed = 20f;
    public float shootDelay = 0.7f;

    [Header("PowerUp - Homing Missiles")]
    public GameObject homingMissilePrefab; 
    public Transform[] missilePoints;
    public float missileSpeed = 25f;
    public int missileDamage = 40;
    public float powerUpCooldown = 5f;
    private bool canPowerUp = true;

    [Header("Skill1 - Falling Bullet")]
    public GameObject skill1BulletPrefab;
    public GameObject impactVFX;
    public float skill1BulletSpeed = 15f;
    public float skill1Cooldown = 5f;
    private bool canUseSkill1 = true;


    public override void Skill_Attack()
    {
        if (!canAttack) return;
        StartCoroutine(SniperShootRoutine());
    }

    public override void Skill_Crouch()
    {

    }

    public override void Skill_PowerUp()
    {
        if (!canPowerUp) return;
        anim.SetTrigger("PowerUp?");
        StartCoroutine(FireHomingMissiles());
        StartCoroutine(PowerUpCooldownRoutine());
    }

    private IEnumerator SniperShootRoutine()
    {
        canAttack = false;
        anim.SetTrigger("Attack?");
        yield return new WaitForSeconds(shootDelay);

        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
                rb.linearVelocity = firePoint.forward * bulletSpeed;
        }

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private IEnumerator FireHomingMissiles()
    {
        canPowerUp = false;
        yield return new WaitForSeconds(1.5f);

        if (homingMissilePrefab != null && missilePoints.Length > 0)
        {
            foreach (Transform mp in missilePoints)
            {
                GameObject missile = Instantiate(homingMissilePrefab, mp.position, mp.rotation);
                HomingMissile hm = missile.GetComponent<HomingMissile>();
                if (hm != null)
                {
                    hm.speed = missileSpeed;
                    hm.damage = missileDamage;
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    private IEnumerator PowerUpCooldownRoutine()
    {
        yield return new WaitForSeconds(powerUpCooldown);
        canPowerUp = true;
    }

    public void Skill1()
    {
        if (!canUseSkill1) return;
        StartCoroutine(Skill1Routine());
    }

    private IEnumerator Skill1Routine()
    {
        canUseSkill1 = false;
        anim.SetTrigger("Attack?");
        yield return new WaitForSeconds(0.7f);

        if (skill1BulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(skill1BulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb == null)
                rb = bullet.AddComponent<Rigidbody>();
            rb.useGravity = true;
            rb.linearVelocity = firePoint.forward * skill1BulletSpeed;

            BulletSkill1 bulletScript = bullet.GetComponent<BulletSkill1>();
            if (bulletScript == null)
                bulletScript = bullet.AddComponent<BulletSkill1>();

            bulletScript.impactVFX = impactVFX;
        }

        yield return new WaitForSeconds(skill1Cooldown);
        canUseSkill1 = true;
    }
}
