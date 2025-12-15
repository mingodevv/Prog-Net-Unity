using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class AssassinClass : HeroCore
{
    [Header("Meshes à rendre invisibles")]
    public List<SkinnedMeshRenderer> skinnedMeshes = new List<SkinnedMeshRenderer>();
    public List<MeshRenderer> meshRenderers = new List<MeshRenderer>();

    [Header("Shotgun Settings")]
    public GameObject shotgunPrefab;
    public Transform firePoint;
    public float shootDelay = 0.2f;
    public float attackCooldown = 0.4f;
    public float bulletSpeed = 5f;

    protected override void HandleActions()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && canAttack)
        {
            StartCoroutine(ShotgunShootRoutine());
        }
        
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            bool crouch = !anim.GetBool("Crouch?");
            anim.SetBool("Crouch?", crouch);
            SetMeshesVisible(!crouch);
        }
        
        if (Keyboard.current.qKey.wasPressedThisFrame)
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

            foreach (Rigidbody rb in bullets)
            {
                rb.linearVelocity = firePoint.forward * bulletSpeed;
            }
        }
        
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private void SetMeshesVisible(bool visible)
    {
        foreach (var mesh in skinnedMeshes)
            if (mesh != null) mesh.enabled = visible;

        foreach (var mesh in meshRenderers)
            if (mesh != null) mesh.enabled = visible;
    }
}
