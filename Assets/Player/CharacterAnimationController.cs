using System;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterAnimationController : MonoBehaviour
{
    [SerializeField] 
    private Animator animPlayer;

    private void Update()
    {
        animPlayer.Play("Walking");
    }

    public void PlayAnimationWalk()
    {
        
    }
}
