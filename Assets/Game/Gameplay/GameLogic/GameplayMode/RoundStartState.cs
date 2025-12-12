using System.Collections.Generic;
using Game.GameState; 

namespace Game.Gameplay.GameLogic
{
    public class RoundStartState : BaseState<GameState.GameState>
    {
        private GameManager _gameManager; 
        public RoundStartState(GameManager gm) : base(GameState.GameState.RoundStart)
        {
            _gameManager = gm; 
        }

        public override void EnterState()
        {
            _gameManager.Manager.RoundSet(_gameManager.Gamedata);
            
            _gameManager.SetTimeRemaining(_gameManager.Gamedata.RoundTime);
        }

        public override void ExitState()
        {
            _gameManager.Manager.endRound = false; 
            _gameManager.Manager.RoundEnd();
        }

        public override void UpdateState()
        {
            _gameManager.Manager.RoundUpdate();
        }

        public override GameState.GameState GetNextState()
        {
            if (_gameManager.TimeRemaining <=0)
                return GameState.GameState.RoundEnd;
            return GameState.GameState.RoundStart;
        }

        public override void OnTriggerEnter() {}
        public override void OnTriggerStay() {}
        public override void OnTriggerExit() {}
    }
}