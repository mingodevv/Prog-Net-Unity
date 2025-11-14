using System.Collections.Generic;
using Game.GameState; 

namespace Game.Gameplay.GameLogic
{
    public class RoundTransitionState : BaseState<GameState.GameState>
    {
        private List<Player> _players; 
        public RoundTransitionState() : base(GameState.GameState.RoundTransition)
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
            return GameState.GameState.RoundTransition;
            // Penser à mettre un next state ici si les conditions pour aller au prochain state sont réunie
        }

        public override void OnTriggerEnter() {}
        public override void OnTriggerStay() {}
        public override void OnTriggerExit() {}
    }
}