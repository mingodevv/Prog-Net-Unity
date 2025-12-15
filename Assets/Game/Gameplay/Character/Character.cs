using System;
using Game.Gameplay.GameLogic;
using Game.GameState;
using Unity.Netcode;
using UnityEngine;

public abstract class Character : NetworkBehaviour
{
    [Header("UI (optionnel)")]
    [SerializeField] private OrbHoldUI orbHoldUI;
    
    protected CharacterMovementController _cmc;
    protected CharacterAnimationController _cac;
    protected CharacterSkillController _csc;

    private readonly NetworkVariable<int> m_orbCount = new();
    private NetworkVariable<int> _teamTag = new();
    
    public CharacterMovementController MovementController => _cmc;
    public CharacterAnimationController AnimationController => _cac;
    public CharacterSkillController SkillController => _csc;

    public int TeamTag
    {
        get => _teamTag.Value;
        set => _teamTag.Value = value;
    }

    public int OrbCount => m_orbCount.Value;

    protected override void OnNetworkPostSpawn()
    {
        base.OnNetworkPostSpawn();

        if (IsOwner && InputController.Instance != null)
        {
            InputController.Instance.SetCharacter(this);
        }
    }

    protected virtual void Awake()
    {
        _cmc = GetComponent<CharacterMovementController>();
        _cac = GetComponent<CharacterAnimationController>();
        _csc = GetComponent<CharacterSkillController>();
    }

    protected virtual void Start()
    {
        if (orbHoldUI != null)
            orbHoldUI.OnUpdateOrbCount(OrbCount);

        m_orbCount.OnValueChanged += HandleOrbCountChangeEvent;
        _teamTag.OnValueChanged += HandleTeamTagChangeEvent;
    }

    public void OnHit(Character p, int damage)
    {
        m_orbCount.Value = OrbCount - damage;
    }

    public void ResetPlayer()
    {
        m_orbCount.Value = 0;
        OnHit(null, 0);
    }

    public void Collected(Collectible c) { }

    public void Collected(VictoryOrb c)
    {
        m_orbCount.Value = OrbCount + c.Value;
    }

    private void HandleOrbCountChangeEvent(int previousValue, int newValue)
    {
        if (orbHoldUI != null)
            orbHoldUI.OnUpdateOrbCount(newValue);
    }

    private void HandleTeamTagChangeEvent(int previousValue, int newValue)
    {
        if (IsOwner && GameStateMachine.Instance != null)
        {
            GameStateMachine.Instance.Gm.MainTeam = newValue;
        }
    }

    public abstract void Skill_Attack();
    public abstract void TakeDamage(int dmg);
}
