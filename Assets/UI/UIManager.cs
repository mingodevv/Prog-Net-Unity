using Unity.Netcode;
using UnityEngine;

public class UIManager : NetworkBehaviour
{
    [Header("UI")] 
    [SerializeField] private UIRoundTransition uiRoundTransition; 
    [SerializeField] private UIRoundEnd uiRoundEnd;
    [SerializeField] private UIGameEnd uiGameEnd;

    public UIRoundTransition RoundTransition => uiRoundTransition;
    public UIRoundEnd RoundEnd => uiRoundEnd;
    public UIGameEnd GameEnd => uiGameEnd;

    public void UpdateUi_Server()
    {
        
    }
    
    public void UpdateUi_Client()
    {
        
    }
}
