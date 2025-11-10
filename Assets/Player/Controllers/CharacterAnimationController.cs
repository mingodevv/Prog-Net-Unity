using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animPlayer;
    
    public void SetSpeed(float speed)
    {
        animPlayer.SetFloat("Speed", speed);
    }
    
    public void SetRolling()
    {
        animPlayer.SetTrigger("isRolling");
    }
}