using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UIRoundEnd : MonoBehaviour
{
    [Header("UI")] 
    [SerializeField] 
    private Image panel;
    
    [SerializeField] 
    private TextMeshProUGUI text;

    private float _timer;
    
    public float Timer
    {
        get => _timer;
        set => _timer = value;
    }

    public void SetUi(int MainClientTeam, int WhoWon)
    {
        if (MainClientTeam == WhoWon)
        {
            text.text = "VICTORY"; 
            panel.color = Color.chartreuse * new Vector4(1, 1, 1,0.2f);
        }
        else
        {
            panel.color = Color.brown * new Vector4(1, 1, 1,0.2f);
        }

        if (WhoWon == 0)
        {
            text.text = "DRAW";
        }
        else
        {
            text.text = "DEFEAT"; 
        }
    }
    
    public void UpdateUI()
    {
        if (_timer <= 0)
            return; 
        
        _timer -= Time.deltaTime;
        
    }
    
}
