using System;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterAnimationController : MonoBehaviour
{
    [SerializeField] 
    private Animator animPlayer;

    public void setWalking(bool cond)
    {
        animPlayer.SetBool("isWalking", cond);
    }
    
    public void setRolling()
    {
        animPlayer.SetTrigger("isRolling");
    }
}
