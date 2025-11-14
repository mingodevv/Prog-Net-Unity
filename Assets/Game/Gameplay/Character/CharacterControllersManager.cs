using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterControllersManager : NetworkBehaviour
{
    [SerializeField]
    private Character m_CharacterPrefab;

    private Dictionary<ulong, Character> m_Characters = new Dictionary<ulong, Character>();
    
    public override void OnDestroy()
    {
        NetworkManager.OnClientConnectedCallback -= HandleClientStarted;
        NetworkManager.OnClientDisconnectCallback -= HandleClientStopped;
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        
        /*
         * L'event NetworkManager.OnClientConnectedCallback n'est pas appelé pour la partie cliente de l'host donc on appelle manuellement le callback.
         */
        
        NetworkManager.OnClientConnectedCallback += HandleClientStarted;
        NetworkManager.OnClientDisconnectCallback += HandleClientStopped;
        
        var clientsEnumerator = NetworkManager.ConnectedClients.GetEnumerator();
        
        // On parcourt tous les éléments du dictionnaire de clients...
        while (clientsEnumerator.MoveNext())
        {
            // Pour chaque pair, on appelle le callback d'arrivée du client pour lui créer un champion.
            var clientPair = clientsEnumerator.Current;
            HandleClientStarted(clientPair.Key);
        }
        
        clientsEnumerator.Dispose();
    }

    private void HandleClientStarted(ulong a_clientId)
    {
        if (!NetworkManager.IsServer)
            return;

        Character newCharacter;
        
        // On cherche à récupérer le champion du client qui vient de se connecter
        
        if (m_Characters.ContainsKey(a_clientId))
        // Si le champion du client existe déjà...
        {
            // On le récupère.
            newCharacter = m_Characters[a_clientId];
        }
        else
        // si il n'existe pas encore...
        {
            // On le crée...
            newCharacter = Instantiate(m_CharacterPrefab, new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f)), Quaternion.identity);
            // et on l'enregistre.
            m_Characters.Add(a_clientId, newCharacter);
        }
        
        // Ici, le champion a bien été récupéré.
        
        if (!newCharacter.IsSpawned)
        // Si le champion n'est pas spawn sur le réseau...
        {
            // on le spawn sur le réseau en lui donnant le client en Owner.
            newCharacter.NetworkObject.SpawnWithOwnership(a_clientId);
        }
        else
        // Si le champion est bien sur le réseau...
        {
            // On lui redonne le client en Owner par sécurité.
            newCharacter.NetworkObject.ChangeOwnership(a_clientId);
        }
    }

    private void HandleClientStopped(ulong a_clientId)
    {
        if (!NetworkManager.IsServer)
            return;

        // Si le champion n'est pas enregistré, on ne fait rien.
        if (!m_Characters.ContainsKey(a_clientId))
            return;
        
        // On récupère le champion...
        var Character = m_Characters[a_clientId];
        // on l'enlève de la liste...
        m_Characters.Remove(a_clientId);
        // on le supprime.
        Destroy(Character.gameObject);
    }
}
