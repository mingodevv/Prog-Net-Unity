using System.Collections.Generic;
using Game.GameState;
using Unity.VisualScripting;

namespace Game.Gameplay.GameLogic
{
    public class GameStateMachine : StateManager<GameState.GameState>
    {
        // J'initialise ici pour passer la référence des liste de joueurs entre états. 
        public List<Player> players; 
        
        void Awake()
        {
            // Ici j'ajoute les états pour ochestrers leurs comportement. Les comportements vont être définie dans leurs State respective
            States.Add(GameState.GameState.GameStart, new GameStartState(players));
            States.Add(GameState.GameState.RoundStart, new RoundStartState());
            States.Add(GameState.GameState.RoundEnd, new RoundEndState());
            States.Add(GameState.GameState.RoundTransition, new RoundStartState());
            States.Add(GameState.GameState.GameEnd, new RoundStartState());
            
            //Ensuite j'indique quel State on est. 
            CurrentState = States[GameState.GameState.GameStart];
        }
    }
}