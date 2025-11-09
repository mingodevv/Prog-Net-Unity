using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header("Paramètres de l'Orbe")]
    public string collectibleName = "Orbe";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"{collectibleName} ramassé !");
            GameManager.Instance.AddCollectible(this);
            Destroy(gameObject);
        }
    }
}
