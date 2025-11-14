using UnityEngine;

public class VictoryOrb : Collectible
{
    private int _value = 1;
    public int Value => _value; 

    public override void OnCollect(Character a_p)
    {
        a_p.Collected(this);
        Destroy(gameObject);
    }
}