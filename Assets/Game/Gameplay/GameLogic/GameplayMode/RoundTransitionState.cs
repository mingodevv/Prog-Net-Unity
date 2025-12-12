using System.Collections.Generic;
using Game.GameState;
using UnityEngine;

namespace Game.Gameplay.GameLogic
{
    public class RoundTransitionState : BaseState<GameState.GameState>
    {
        private UIRoundTransition _uiRound;
        private GameplayMode _data;
        private GameManager _gameManager;
        private List<Character> _players;
        public RoundTransitionState(UIManager uiManager, List<Character> players, GameManager gm) : base(GameState.GameState.RoundTransition)
        {
            _uiRound = uiManager.RoundTransition;
            _data = gm.Gamedata;
            _players = players;
            _gameManager = gm;
        }

        public override void EnterState()
        {
            _uiRound.UpdateUI(true);
            _gameManager.SetTimeRemaining(4);
            
            foreach (var character in  _players)
            {
                character.ResetPlayer();
                // mettre des spawnpoints défini pour les players. 
            }
            
        }

        public override void ExitState()
        {
            _uiRound.UpdateUI(false);
        }

        public override void UpdateState()
        {
            _uiRound.TimerSet(_gameManager.TimeRemaining);
        }

        public override GameState.GameState GetNextState()
        {
            if (_gameManager.Manager.RoundDone -1 >= _data.RoundNumber)
                return GameState.GameState.GameEnd;
            if(_gameManager.TimeRemaining <=0)
                return GameState.GameState.RoundStart;
            return GameState.GameState.RoundTransition;
        }

        public override void OnTriggerEnter() {}
        public override void OnTriggerStay() {}
        public override void OnTriggerExit() {}
    }
}