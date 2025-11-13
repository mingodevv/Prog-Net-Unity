using System;
using UnityEngine;

public abstract class Collectible : MonoBehaviour
{
    [Header("Paramètres de l'Orbe")] 
    [SerializeField] private string collectibleName = "Orbe";

    //Timer pour définir 
    [SerializeField] private float timerCanGrabOrb = 0f;
    public float TimerCanGrabOrb
    {
        get => timerCanGrabOrb;
        set => timerCanGrabOrb = value;
    }
    
    public void Update()
    {
        //Si le timer est inférieur à 0, le joueur peut grab l'orbe
        if (timerCanGrabOrb > 0f)
        {
            timerCanGrabOrb -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && timerCanGrabOrb <= 0f) ; 
        {
            OnCollect(other.gameObject.GetComponent<Player>());
        }
    }
    
    public abstract void OnCollect(Player a_p);
}