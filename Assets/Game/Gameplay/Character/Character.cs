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

    public CharacterMovementController MovementController => _cmc;
    public CharacterAnimationController AnimationController => _cac;
    public CharacterSkillController SkillController => _csc;

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
        orbHoldUI.OnUpdateOrbCount(_orbCount);

        _cmc.CharacterRigidbody = _rigidbodyToPass; 
    }

    public void OnHit(Character p, int damage)
    {
        _orbCount -= damage; 
        //TODO: Add Instantiate orb drops to 
    }
    
    public void Collected(Collectible c)
    {
        // Surcharge 
    }
    
    //Surchage de la fonction Collected au cas ou le Collected peut être utile :) 
    public void Collected(VictoryOrb c)
    {
        _orbCount += c.Value;
        orbHoldUI.OnUpdateOrbCount(_orbCount);
    }
}
