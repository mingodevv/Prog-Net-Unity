using UnityEngine;

public class VictoryOrb : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Le joueur a ramassé l’orbe !");
        
            if (GameEndUIManager.Instance != null)
            {
                Debug.Log("GameEndUIManager détecté, affichage victoire...");
                GameEndUIManager.Instance.ShowVictory();
            }
            else
            {
                Debug.LogError("GameEndUIManager.Instance est NULL !");
            }

            Destroy(gameObject);
        }
    }

}