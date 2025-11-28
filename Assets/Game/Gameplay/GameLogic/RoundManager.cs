using System.Collections.Generic;
using Game.Gameplay.GameLogic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class RoundManager : NetworkBehaviour
{
    private bool _isSet; 
    private NetworkVariable<float> _timeRound = new(); 
    private float _orbIntervalSpawn;
    private float _orbTimer;
    private Vector3 _orbSpawnSet;
    private NetworkVariable<int>  _roundDone = new();

    [SerializeField]
    private Collectible _orbObject;

    
    private List<Collectible> _collectiblesList; 

    public bool endRound;

    public float TimeRound => _timeRound.Value;
    public int RoundDone => _roundDone.Value;
    
    public void RoundSet(GameplayMode gameplayMode)
    {
        if (!IsServer)
            return;
        _timeRound.Value = gameplayMode.RoundTime;
        _orbIntervalSpawn = gameplayMode.OrbSpawnInterval;
        _orbTimer = _orbIntervalSpawn; 
        _orbSpawnSet = gameplayMode.OrbSpawnLocation;
        _isSet = true;
        _roundDone.Value++;
    }
    
    public void RoundUpdate()
    {
        if (!IsServer)
            return;
        if (!_isSet)
            return; 
        _timeRound.Value = TimeRound - Time.deltaTime;
        _orbTimer -= Time.deltaTime;
        if (TimeRound <= 0)
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
        if (!IsServer)
            return;
        _isSet = false;
        endRound = true;
        DespawnAllCollectible();
    }

    public void SpawnOrb(Vector3 location)
    {
        if (!IsServer)
            return;
        Collectible newCollectible; // Déclaration de variable au cas où ça sera utile de l'avoir plus tard.
        newCollectible = Instantiate(_orbObject, new Vector3(Random.Range(-1f, 1f) + location.x, 1f+ location.y, Random.Range(-1f, 1f) + location.z), Quaternion.identity);
        newCollectible.NetworkObject.Spawn();
        _collectiblesList.Add(newCollectible);
    }

    public void DespawnAllCollectible()
    {
        if (!IsServer)
            return;
        foreach (var collectible in _collectiblesList)
        {
            Destroy(collectible);
        }
    }
}
