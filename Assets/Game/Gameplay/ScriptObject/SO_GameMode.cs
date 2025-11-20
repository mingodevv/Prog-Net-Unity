using UnityEngine;

[CreateAssetMenu(fileName = "GameplayMode", menuName = "GameMode/GameplayMode")]
public class GameplayMode : ScriptableObject
{
    [Header("Game Mode Setting")] 
    [SerializeField]
    private string _gameModeName;
    [SerializeField]
    private int _numberPlayerRequired;
    [SerializeField]
    private float _orbSpawnInterval;
    [SerializeField]
    private float _roundTime;
    [SerializeField]
    private int _roundNumber;
    [SerializeField] 
    private Vector3 _orbSpawnLocation;  

    public string GameModeName => _gameModeName;

    public int NumberPlayerRequired => _numberPlayerRequired;

    public float OrbSpawnInterval => _orbSpawnInterval;

    public float RoundTime => _roundTime;

    public int RoundNumber => _roundNumber;

    public Vector3 OrbSpawnLocation => _orbSpawnLocation;
}
