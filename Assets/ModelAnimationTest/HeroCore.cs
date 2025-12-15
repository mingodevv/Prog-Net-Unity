using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using Game.Gameplay.GameLogic;
using Unity.Netcode;

[RequireComponent(typeof(CharacterController))]
public class HeroCore : Character
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
    
    private readonly NetworkVariable<int> m_orbCount = new();
    private NetworkVariable<int> _teamTag = new();

    [Header("UI")] 
    [SerializeField] private OrbHoldUI _orbUi; 
    
    //Pas le temps de faire un truc correct pour adapter tout les controllers dans hero donc ; 
    private bool isControlled; 
    
    public int TeamTag
    {
        get => _teamTag.Value;
        set => _teamTag.Value = value;
    }

    public int OrbCount => m_orbCount.Value;
    
    protected override void OnNetworkPostSpawn()
    {
        base.OnNetworkPostSpawn();

        if (IsOwner)
        {
            isControlled= true;
        }
    }

    protected virtual void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        
        m_orbCount.OnValueChanged += HandleOrbCountChangeEvent;
        _teamTag.OnValueChanged += HandleTeamTagChangeEvent; 

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
        if (Mouse.current.leftButton.wasPressedThisFrame && canAttack && isControlled)
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
    
    public void ResetPlayer()
    {
        m_orbCount.Value = 0;
    }
    
    public void Collected(Collectible c)
    {
        // Surcharge 
    }
    
    //Surchage de la fonction Collected au cas ou le Collected peut être utile :) 
    public void Collected(VictoryOrb c)
    {
        m_orbCount.Value = OrbCount + c.Value;
    }
    
    private void HandleOrbCountChangeEvent(int previousValue, int newValue)
    {
        _orbUi.OnUpdateOrbCount(OrbCount);
    }
    
    private void HandleTeamTagChangeEvent(int previousValue, int newValue)
    {
        _teamTag.Value = newValue;
        if (IsOwner)
        {
            GameStateMachine.Instance.Gm.MainTeam = newValue; 
        }
    }
}
