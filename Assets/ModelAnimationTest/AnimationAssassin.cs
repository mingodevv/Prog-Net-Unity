using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CharacterController))]
public class AnimationAssassinController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 3f;
    public float runSpeed = 6f;

    [Header("Meshes à rendre invisibles")]
    public List<SkinnedMeshRenderer> skinnedMeshes = new List<SkinnedMeshRenderer>();
    public List<MeshRenderer> meshRenderers = new List<MeshRenderer>();

    [Header("Attack Cooldown")]
    public float attackCooldown = 0.8f;
    private bool canAttack = true;

    private CharacterController controller;
    private Animator anim;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        HandleMovement();
        HandleActions();
    }

    private void HandleMovement()
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

    private void HandleActions()
    {
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            bool crouch = !anim.GetBool("Crouch?");
            anim.SetBool("Crouch?", crouch);

            SetMeshesVisibility(!crouch);
        }
        
        if (Mouse.current.leftButton.wasPressedThisFrame && canAttack)
        {
            anim.SetTrigger("Attack?");
            StartCoroutine(AttackCooldownRoutine());
        }
        
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            anim.SetTrigger("PowerUp?");
        }
    }

    private void SetMeshesVisibility(bool visible)
    {
        foreach (var mesh in skinnedMeshes)
        {
            if (mesh != null)
                mesh.enabled = visible;
        }

        foreach (var mesh in meshRenderers)
        {
            if (mesh != null)
                mesh.enabled = visible;
        }
    }

    private IEnumerator AttackCooldownRoutine()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
