using Unity.Netcode;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : NetworkBehaviour
{
    [Header("Game Settings")] 
    [SerializeField]
    private GameplayMode gamedata;

    [SerializeField] 
    private RoundManager roundManager; 

    private float _timeRemaining;

    public RoundManager Manager => roundManager;
    public GameplayMode Gamedata => gamedata;
    public float TimeRemaining => _timeRemaining;

    public void SetTimeRemaining(float TimeSet)
    {
        _timeRemaining = TimeSet;
    }

    public float UpdateTimer()
    {
        if (TimeRemaining <= 0)
            return 0;
        _timeRemaining = TimeRemaining - Time.deltaTime;
        return TimeRemaining;
    }
}