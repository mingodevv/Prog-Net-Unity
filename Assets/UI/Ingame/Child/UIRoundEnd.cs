using System;
using TMPro;
using UI.Ingame;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class UIRoundEnd : UIModel
{
    [Header("UI")] 
    [SerializeField] 
    private Image panel;
    
    [SerializeField] 
    private TextMeshProUGUI text;
    
    private NetworkVariable<FixedString64Bytes> _roundEndString = new();
    
    public NetworkVariable<FixedString64Bytes> RoundEndString
    {
        get => _roundEndString;
        set => _roundEndString = value;
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        RoundEndString.OnValueChanged += HandleRoundEndTextChange;
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

        RoundEndString.Value = text.text; 
    }

    public void HandleRoundEndTextChange(FixedString64Bytes previousValue, FixedString64Bytes newValue)
    {
        if (IsServer)
            return;
        if(newValue == "VICTORY")
            panel.color = Color.chartreuse * new Vector4(1, 1, 1,0.2f);
        else 
            panel.color = Color.brown * new Vector4(1, 1, 1,0.2f);
        text.text = newValue.ToString();
    }
}
