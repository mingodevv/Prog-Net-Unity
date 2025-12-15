using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AssassinClass : HeroCore
{
    [Header("Meshes")]
    public List<SkinnedMeshRenderer> skinnedMeshes = new List<SkinnedMeshRenderer>();
    public List<MeshRenderer> meshRenderers = new List<MeshRenderer>();

    [Header("Shotgun")]
    public GameObject shotgunPrefab;
    public Transform firePoint;
    public float shootDelay = 0.2f;
    public float bulletSpeed = 5f;

    public override void Skill_Attack()
    {
        if (!canAttack) return;
        StartCoroutine(ShotgunShootRoutine());
    }

    public override void Skill_Crouch()
    {
        bool crouch = !anim.GetBool("Crouch?");
        anim.SetBool("Crouch?", crouch);
        SetMeshesVisible(!crouch);
    }

    public override void Skill_PowerUp()
    {
        anim.SetTrigger("PowerUp?");
    }

    private IEnumerator ShotgunShootRoutine()
    {
        canAttack = false;
        anim.SetTrigger("Attack?");
        yield return new WaitForSeconds(shootDelay);

        if (shotgunPrefab != null && firePoint != null)
        {
            GameObject instance = Instantiate(shotgunPrefab, firePoint.position, firePoint.rotation);
            Rigidbody[] bullets = instance.GetComponentsInChildren<Rigidbody>();
            foreach (var rb in bullets)
                rb.linearVelocity = firePoint.forward * bulletSpeed;
        }

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private void SetMeshesVisible(bool visible)
    {
        foreach (var mesh in skinnedMeshes) if (mesh) mesh.enabled = visible;
        foreach (var mesh in meshRenderers) if (mesh) mesh.enabled = visible;
    }
}