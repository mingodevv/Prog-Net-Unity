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

    private NetworkVariable<double> _timeOfStart = new(); 
    private NetworkVariable<float> _totalTime = new(); 
    public RoundManager Manager => roundManager;
    public GameplayMode Gamedata => gamedata;
    public double TimeRemaining => _totalTime.Value - (NetworkManager.ServerTime.Time - _timeOfStart.Value );
    public int MainTeam;

    public void SetTimeRemaining(float TimeSet)
    {
        _timeOfStart.Value = NetworkManager.ServerTime.Time;
        _totalTime.Value = TimeSet; 
    }
}