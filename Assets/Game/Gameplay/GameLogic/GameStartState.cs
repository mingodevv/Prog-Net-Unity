using System.Collections.Generic;
using Game.GameState; 

namespace Game.Gameplay.GameLogic
{
    public class GameStartState : BaseState<GameState.GameState>
    {
        private List<Player> _players; 
        public GameStartState(List<Player> ps) : base(GameState.GameState.GameStart)
        {
            _players = ps; 
        }

        public override void EnterState()
        {
        }

        public override void ExitState()
        {
        }

        public override void UpdateState()
        {
        }

        public override GameState.GameState GetNextState()
        {
            return GameState.GameState.GameStart;
            // Penser à mettre un next state ici si les conditions pour aller au prochain state sont réunie
        }

        public override void OnTriggerEnter() {}
        public override void OnTriggerStay() {}
        public override void OnTriggerExit() {}
    }
}