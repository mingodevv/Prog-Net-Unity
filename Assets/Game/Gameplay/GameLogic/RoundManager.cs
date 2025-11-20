using Game.Gameplay.GameLogic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class RoundManager : MonoBehaviour
{
    private bool _isSet; 
    private float _timeRound;
    private float _orbIntervalSpawn;
    private float _orbTimer;
    private Vector3 _orbSpawnSet;
    private int _roundDone; 

    [SerializeField]
    private Collectible _orbObject; 

    public bool endRound;

    public void RoundSet(GameplayMode gameplayMode)
    {
        _timeRound = gameplayMode.RoundTime;
        _orbIntervalSpawn = gameplayMode.OrbSpawnInterval;
        _orbTimer = _orbIntervalSpawn; 
        _orbSpawnSet = gameplayMode.OrbSpawnLocation;
        _isSet = true;
    }
    
    public void RoundUpdate()
    {
        if (!_isSet)
            return; 
        _timeRound -= Time.deltaTime;
        _orbTimer -= Time.deltaTime;
        if (_timeRound <= 0)
        {
            RoundEnd();
            return; 
        }

        if (_orbTimer <= 0)
        {
            SpawnOrb(_orbSpawnSet);
            _orbTimer = _orbIntervalSpawn; 
        } 
    }

    public void RoundEnd()
    {
        _isSet = false;
        endRound = true; 
    }

    public void SpawnOrb(Vector3 location)
    {
        Collectible newCollectible; // Déclaration de variable au cas où ça sera utile de l'avoir plus tard.
        newCollectible = Instantiate(_orbObject, new Vector3(Random.Range(-1f, 1f) + location.x, 1f+ location.y, Random.Range(-1f, 1f) + location.z), Quaternion.identity); 
    }
}
