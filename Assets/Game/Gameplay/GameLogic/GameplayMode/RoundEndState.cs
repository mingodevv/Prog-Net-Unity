using System.Collections.Generic;
using Game.GameState; 

namespace Game.Gameplay.GameLogic
{
    public class RoundEndState : BaseState<GameState.GameState>
    {
        private List<Character> _players;
        private UIRoundEnd _ui;
        private CharacterControllersManager _ccm; 
        public RoundEndState(List<Character> players ,UIRoundEnd ui, CharacterControllersManager controllersManager) : base(GameState.GameState.RoundEnd)
        {
            _players = players; 
            _ui = ui;
            _ccm = controllersManager;
        }

        public override void EnterState()
        {
            _ui.gameObject.SetActive(true);
            _ui.Timer = 5;
            _ui.SetUi(_players[0].TeamTag, _ccm.TeamLead());
        }

        public override void ExitState()
        {
            _ui.gameObject.SetActive(false);
        }

        public override void UpdateState()
        {
            _ui.UpdateUI();
        }

        public override GameState.GameState GetNextState()
        {
            if (_ui.Timer <= 0)
                return GameState.GameState.RoundTransition; 
            return GameState.GameState.RoundEnd;
        }

        public override void OnTriggerEnter() {}
        public override void OnTriggerStay() {}
        public override void OnTriggerExit() {}
    }
}