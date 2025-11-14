using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterControllersManager : NetworkBehaviour
{
    [SerializeField]
    private CharacterMovementController m_CharacterMovementControllerPrefab;

    private Dictionary<ulong, CharacterMovementController> m_CharacterMovementControllers = new Dictionary<ulong, CharacterMovementController>();
    
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

        CharacterMovementController newCharacterMovementController;
        
        // On cherche à récupérer le champion du client qui vient de se connecter
        
        if (m_CharacterMovementControllers.ContainsKey(a_clientId))
        // Si le champion du client existe déjà...
        {
            // On le récupère.
            newCharacterMovementController = m_CharacterMovementControllers[a_clientId];
        }
        else
        // si il n'existe pas encore...
        {
            // On le crée...
            newCharacterMovementController = Instantiate(m_CharacterMovementControllerPrefab, new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f)), Quaternion.identity);
            // et on l'enregistre.
            m_CharacterMovementControllers.Add(a_clientId, newCharacterMovementController);
        }
        
        // Ici, le champion a bien été récupéré.
        
        if (!newCharacterMovementController.IsSpawned)
        // Si le champion n'est pas spawn sur le réseau...
        {
            // on le spawn sur le réseau en lui donnant le client en Owner.
            newCharacterMovementController.NetworkObject.SpawnWithOwnership(a_clientId);
        }
        else
        // Si le champion est bien sur le réseau...
        {
            // On lui redonne le client en Owner par sécurité.
            newCharacterMovementController.NetworkObject.ChangeOwnership(a_clientId);
        }
    }

    private void HandleClientStopped(ulong a_clientId)
    {
        if (!NetworkManager.IsServer)
            return;

        // Si le champion n'est pas enregistré, on ne fait rien.
        if (!m_CharacterMovementControllers.ContainsKey(a_clientId))
            return;
        
        // On récupère le champion...
        var CharacterMovementController = m_CharacterMovementControllers[a_clientId];
        // on l'enlève de la liste...
        m_CharacterMovementControllers.Remove(a_clientId);
        // on le supprime.
        Destroy(CharacterMovementController.gameObject);
    }
}
