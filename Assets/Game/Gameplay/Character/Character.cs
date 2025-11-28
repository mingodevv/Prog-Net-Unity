using System;
using Unity.Netcode;
using UnityEngine;

public class Character : NetworkBehaviour
{
    [Header("Références")]
    [SerializeField] private OrbHoldUI orbHoldUI;
    [SerializeField] private CharacterMovementController _cmc;
    [SerializeField] private CharacterAnimationController _cac;
    [SerializeField] private CharacterSkillController _csc; 
    [SerializeField] private Rigidbody _rigidbodyToPass; 
    
    private readonly NetworkVariable<int> m_orbCount = new();
    private int _teamTag = 0; 

    public CharacterMovementController MovementController => _cmc;
    public CharacterAnimationController AnimationController => _cac;
    public CharacterSkillController SkillController => _csc;
    public int TeamTag
    {
        get => _teamTag;
        set => _teamTag = value;
    }

    public int OrbCount => m_orbCount.Value;
    protected override void OnNetworkPostSpawn()
    {
        base.OnNetworkPostSpawn();

        if (IsOwner)
        {
            InputController.Instance.SetCharacter(this);
        }
    }
    
    public void Start()
    {
        orbHoldUI.OnUpdateOrbCount(OrbCount);

        _cmc.CharacterRigidbody = _rigidbodyToPass; 
        m_orbCount.OnValueChanged += HandleOrbCountChangeEvent;
    }

    public void OnHit(Character p, int damage)
    {
        m_orbCount.Value = OrbCount - damage; 
        //TODO: Add Instantiate orb drops to 
    }

    public void ResetPlayer()
    {
        m_orbCount.Value = 0;
        OnHit(null, 0);
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
        orbHoldUI.OnUpdateOrbCount(OrbCount);
    }
    
}
