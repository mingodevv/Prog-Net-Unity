using UnityEngine;
using TMPro;
using UI.Ingame;
using UnityEngine.UI;

public class UIGameEnd : UIModel
{
    private bool _isRestarting;

    public bool IsRestarting
    {
        get => _isRestarting;
        set => _isRestarting = value;
    }


    public override void UpdateUI(bool cond)
    {
        this.gameObject.SetActive(cond);
    }

    public override void TimerSet(float timer)
    {
        
    }
}