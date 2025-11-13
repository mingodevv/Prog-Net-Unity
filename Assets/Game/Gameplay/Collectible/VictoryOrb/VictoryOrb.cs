using UnityEngine;

public class VictoryOrb : Collectible
{
    private int _value = 1;
    public int Value => _value; 

    public override void OnCollect(Player a_p)
    {
        a_p.Collected(this);
        enabled = false; 
        Destroy(gameObject);
    }
}