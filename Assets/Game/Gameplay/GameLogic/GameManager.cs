using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Settings")]
    public int collectiblesToWin = 1;
    public float timeLimit = 15f;

    private int _collectiblesCollected;
    private float _timeRemaining;
    private bool _gameEnded;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        _timeRemaining = timeLimit;
    }

    private void Update()
    {
        if (_gameEnded) return;

        // Timer
        // _timeRemaining -= Time.deltaTime;

        if (GameEndUIManager.Instance != null)
            GameEndUIManager.Instance.UpdateTimer(_timeRemaining);

        if (_timeRemaining <= 0)
        {
            LoseGame();
        }
    }

    public void AddCollectible(Collectible collectible)
    {
        if (_gameEnded) return;

        _collectiblesCollected++;
        Debug.Log($"Orbe ramassé ({_collectiblesCollected}/{collectiblesToWin})");

        if (_collectiblesCollected >= collectiblesToWin)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        if (_gameEnded) return;
        _gameEnded = true;

        Debug.Log("Victory");
        if (GameEndUIManager.Instance != null)
            GameEndUIManager.Instance.ShowVictory();
    }

    public void LoseGame()
    {
        if (_gameEnded) return;
        _gameEnded = true;

        Debug.Log("Defeat");
        if (GameEndUIManager.Instance != null)
            GameEndUIManager.Instance.ShowDefeat();
    }
}