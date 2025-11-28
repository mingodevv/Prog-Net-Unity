using UnityEngine;

public class VictoryOrb : Collectible
{
    private int _value = 1;
    public int Value => _value;


    public override void OnCollect_Client(Character a_p)
    {
    }

    public override void OnCollect_Server(Character a_p)
    {
        a_p.Collected(this);
        Destroy(gameObject);
    }

    // public override void OnCollect(Character a_p)
    // {
    //     a_p.Collected(this);
    //     if (IsServer)
    //     {
    //         Destroy(gameObject);
    //     }
    // }
}