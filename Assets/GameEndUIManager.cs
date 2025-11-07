using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameEndUIManager : MonoBehaviour
{
    public static GameEndUIManager Instance;

    [Header("Références UI")]
    [SerializeField] private GameObject panelEndGame;
    [SerializeField] private TextMeshProUGUI textVictoryDefeat;
    [SerializeField] private Button restartButton;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
        if (panelEndGame != null)
            panelEndGame.SetActive(false);
        
        if (restartButton != null)
            restartButton.onClick.AddListener(RestartGame);
    }

    public void ShowVictory()
    {
        ShowEndScreen("Victory");
    }

    public void ShowDefeat()
    {
        ShowEndScreen("Defeat");
    }

    private void ShowEndScreen(string message)
    {
        if (panelEndGame == null || textVictoryDefeat == null)
        {
            Debug.LogError("Référence UI manquante dans GameEndUIManager !");
            return;
        }

        panelEndGame.SetActive(true);
        textVictoryDefeat.text = message;

        Time.timeScale = 0f;
    }

    private void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}