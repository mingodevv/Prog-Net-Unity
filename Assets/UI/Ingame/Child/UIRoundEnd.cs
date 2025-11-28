using TMPro;
using UI.Ingame;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UIRoundEnd : UIModel
{
    [Header("UI")] 
    [SerializeField] 
    private Image panel;
    
    [SerializeField] 
    private TextMeshProUGUI text;

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

    public override void UpdateUI(bool cond)
    {
        gameObject.SetActive(cond);
    }

    public override void TimerSet(float timer)
    {
        throw new System.NotImplementedException();
    }
}
