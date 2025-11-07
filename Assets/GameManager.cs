using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int _collectiblesCollected;
    public int collectiblesToWin = 1;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void AddCollectible(Collectible collectible)
    {
        _collectiblesCollected++;
        Debug.Log($"Orbe ramassé ({_collectiblesCollected}/{collectiblesToWin})");

        if (_collectiblesCollected >= collectiblesToWin)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        Debug.Log("Condition de victoire atteinte ! Victoire !");
        
        if (GameEndUIManager.Instance != null)
        {
            GameEndUIManager.Instance.ShowVictory();
        }
        else
        {
            Debug.LogWarning("Aucun GameEndUIManager trouvé dans la scène !");
        }
    }

    public void LoseGame()
    {
        Debug.Log("Défaite !");
        if (GameEndUIManager.Instance != null)
        {
            GameEndUIManager.Instance.ShowDefeat();
        }
    }
}