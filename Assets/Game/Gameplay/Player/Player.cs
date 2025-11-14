using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Références")]
    [SerializeField] private OrbHoldUI orbHoldUI;
    
    private int _orbCount =0;

    public void Start()
    {
        orbHoldUI.OnUpdateOrbCount(_orbCount);
    }

    public void OnHit(Player p, int damage)
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
