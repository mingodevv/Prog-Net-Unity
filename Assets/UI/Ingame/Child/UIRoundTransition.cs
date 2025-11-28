using TMPro;
using UI.Ingame;
using UnityEngine;
using UnityEngine.Serialization;

public class UIRoundTransition : UIModel
{
    [Header("UI")] 
    [SerializeField] private TextMeshProUGUI roundText;

    [SerializeField] private RoundManager _roundManager; 
    
    public override void UpdateUI(bool cond)
    {
        gameObject.SetActive(cond);
    }

    public override void TimerSet(float timer)
    {
        if(timer <=0 )
            return;
        if (timer <= 1)
        {
            roundText.text = "ROUND " + _roundManager.RoundDone + " START";
        }
        else
        {
            int time = (int)timer; 
            roundText.text = time.ToString(); 
        }
    }
}
