using TMPro;
using UnityEngine;

public class UIRoundTransition : MonoBehaviour
{
    [Header("UI")] 
    [SerializeField] private TextMeshProUGUI text;

    private float _timer;
    private int _roundCount; 
    
    public float Timer
    {
        get => _timer;
        set => _timer = value;
    }
    
    public int RoundCount
    {
        get => _roundCount;
        set => _roundCount = value;
    }

    public void UpdateTransition()
    {
        _timer -= Time.deltaTime;
        if(_timer <=0 )
            return;
        if (_timer <= 1)
        {
            text.text = "ROUND " + RoundCount + " START";
        }
        else
        {
            int time = (int)_timer; 
            text.text = time.ToString(); 
            
        }
    }
}
