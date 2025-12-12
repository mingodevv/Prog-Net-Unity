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

    public void Restart()
    {
        
    }
}