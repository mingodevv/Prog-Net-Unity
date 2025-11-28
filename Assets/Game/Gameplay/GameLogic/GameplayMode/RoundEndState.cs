using System.Collections.Generic;
using Game.GameState; 

namespace Game.Gameplay.GameLogic
{
    public class RoundEndState : BaseState<GameState.GameState>
    {
        private List<Character> _players;
        private UIRoundEnd _ui;
        private CharacterControllersManager _ccm;
        private GameManager _gameManager; 
        public RoundEndState(List<Character> players ,UIManager ui, CharacterControllersManager controllersManager, GameManager gm) : base(GameState.GameState.RoundEnd)
        {
            _players = players; 
            _ui = ui.RoundEnd;
            _ccm = controllersManager;
            _gameManager = gm; 
        }

        public override void EnterState()
        {
            _ui.UpdateUI(true);
            _gameManager.SetTimeRemaining(5);
            _ui.SetUi(_players[0].TeamTag, _ccm.TeamLead());
        }

        public override void ExitState()
        {
            _ui.UpdateUI(false);
        }

        public override void UpdateState()
        {
            
        }

        public override GameState.GameState GetNextState()
        {
            if (_gameManager.TimeRemaining <= 0)
                return GameState.GameState.RoundTransition; 
            return GameState.GameState.RoundEnd;
        }

        public override void OnTriggerEnter() {}
        public override void OnTriggerStay() {}
        public override void OnTriggerExit() {}
    }
}