using System.Collections.Generic;
using Game.GameState; 

namespace Game.Gameplay.GameLogic
{
    public class RoundStartState : BaseState<GameState.GameState>
    {
        private List<Player> _players; 
        public RoundStartState() : base(GameState.GameState.RoundStart)
        {
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
            return GameState.GameState.RoundStart;
            // Penser à mettre un next state ici si les conditions pour aller au prochain state sont réunie
        }

        public override void OnTriggerEnter() {}
        public override void OnTriggerStay() {}
        public override void OnTriggerExit() {}
    }
}