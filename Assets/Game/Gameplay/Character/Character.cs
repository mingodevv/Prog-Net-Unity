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
    
    private int _orbCount =0;
    private int _teamTag = 0; 

    public CharacterMovementController MovementController => _cmc;
    public CharacterAnimationController AnimationController => _cac;
    public CharacterSkillController SkillController => _csc;
    public int TeamTag
    {
        get => _teamTag;
        set => _teamTag = value;
    }

    public int OrbCount
    {
        get => _orbCount;
        set => _orbCount = value;
    }


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
    }

    public void OnHit(Character p, int damage)
    {
        OrbCount = OrbCount - damage; 
        //TODO: Add Instantiate orb drops to 
    }
    
    public void Collected(Collectible c)
    {
        // Surcharge 
    }
    
    //Surchage de la fonction Collected au cas ou le Collected peut être utile :) 
    public void Collected(VictoryOrb c)
    {
        OrbCount = OrbCount + c.Value;
        orbHoldUI.OnUpdateOrbCount(OrbCount);
    }
}
