using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")] 
    [SerializeField]
    private GameplayMode gamedata;

    [SerializeField] 
    private RoundManager roundManager; 

    private int _collectiblesCollected;
    private float _timeRemaining;
    private bool _gameEnded;

    public RoundManager Manager => roundManager;

    public GameplayMode Gamedata => gamedata;
}