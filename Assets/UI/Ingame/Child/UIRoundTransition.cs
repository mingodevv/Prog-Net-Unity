using System;
using TMPro;
using UI.Ingame;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Serialization;

public class UIRoundTransition : UIModel
{
    [Header("UI")] 
    [SerializeField] private TextMeshProUGUI roundText;

    [SerializeField] private RoundManager _roundManager;

    private NetworkVariable<FixedString64Bytes> _transitionString = new();

    public NetworkVariable<FixedString64Bytes> TransitionString
    {
        get => _transitionString;
        set => _transitionString = value;
   	}

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        TransitionString.OnValueChanged += HandleTransitionTextChange;
    }


    public void TimerSet(double timer)
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

        TransitionString.Value = roundText.text; 
    }
        
	public void HandleTransitionTextChange(FixedString64Bytes previousValue, FixedString64Bytes newValue)
    {
        roundText.text = newValue.ToString();
    }
}
