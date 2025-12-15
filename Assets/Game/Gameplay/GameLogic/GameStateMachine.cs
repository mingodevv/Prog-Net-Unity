using System.Collections.Generic;
using Game.GameState;
using UI.Ingame;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Gameplay.GameLogic
{
    public class GameStateMachine : StateManager<GameState.GameState>
    {
        #region Singleton
        public static GameStateMachine Instance { get; private set; }
        #endregion
        
        // J'initialise ici pour passer la référence des liste de joueurs entre états. 
        public List<Character> players= new List<Character>();
        [SerializeField] private GameManager gm;
        [SerializeField] private CharacterControllersManager ccm;
        [SerializeField] private UIManager uiManager; 
            
        public GameManager Gm => gm;
        public int currentTeam;

        void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }
        
            Instance = this;
            // Ici j'ajoute les états pour ochestrers leurs comportement. Les comportements vont être définie dans leurs State respective
            States.Add(GameState.GameState.GameStart, new GameStartState(players, gm, uiManager));
            States.Add(GameState.GameState.RoundStart, new RoundStartState(gm));
            States.Add(GameState.GameState.RoundEnd, new RoundEndState(players, uiManager, ccm, gm));
            States.Add(GameState.GameState.RoundTransition, new RoundTransitionState(uiManager, players, gm));
            States.Add(GameState.GameState.GameEnd, new GameEndState(uiManager));
            
            //Ensuite j'indique quel State on est. 
            CurrentState = States[GameState.GameState.GameStart];
        }
    }
}