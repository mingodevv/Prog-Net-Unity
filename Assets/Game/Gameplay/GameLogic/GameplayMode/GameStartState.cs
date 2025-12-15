using System.Collections.Generic;
using Game.GameState;
using UI.Ingame;

namespace Game.Gameplay.GameLogic
{
    public class GameStartState : BaseState<GameState.GameState>
    {
        private List<Character> _players;
        private GameManager _gm;
        private UIManager _UIs; 

        public GameStartState(List<Character> ps, GameManager gm, UIManager UIs) : base(GameState.GameState.GameStart)
        {
            _players = ps;
            _gm = gm;
            _UIs = UIs; 
        }

        public override void EnterState()
        {
            _UIs.GameEnd.UpdateUI(false);
            _UIs.RoundEnd.UpdateUI(false);
            _UIs.RoundTransition.UpdateUI(false);
            // Visuel pour montrer le nombre de joueur manquant 
            // Mettre un réinitialisation complête ici ; 
        }

        public override void ExitState()
        {
        }

        public override void UpdateState()
        {
        }

        public override GameState.GameState GetNextState()
        {
            if (_players.Count >= _gm.Gamedata.NumberPlayerRequired)
                return GameState.GameState.RoundTransition; 
            return GameState.GameState.GameStart;
        }

        public override void OnTriggerEnter()
        {
        }

        public override void OnTriggerStay()
        {
        }

        public override void OnTriggerExit()
        {
        }
    }
}