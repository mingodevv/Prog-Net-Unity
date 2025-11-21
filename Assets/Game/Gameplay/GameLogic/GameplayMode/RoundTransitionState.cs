using System.Collections.Generic;
using Game.GameState;
using UnityEngine;

namespace Game.Gameplay.GameLogic
{
    public class RoundTransitionState : BaseState<GameState.GameState>
    {
        private UIRoundTransition _uiRound;
        private GameplayMode _data; 
        
        private List<Character> _players;
        public RoundTransitionState(UIRoundTransition UITransition, GameplayMode data, List<Character> players) : base(GameState.GameState.RoundTransition)
        {
            _uiRound = UITransition;
            _data = data;
            _players = players;
        }

        public override void EnterState()
        {
            _uiRound.gameObject.SetActive(true); 
            _uiRound.RoundCount++;
            _uiRound.Timer = 4;

            foreach (var character in  _players)
            {
                character.OrbCount = 0; 
                character.OnHit(null, 0);
                // mettre des spawnpoints défini pour les players. 
            }
            
        }

        public override void ExitState()
        {
            _uiRound.gameObject.SetActive(false); 
        }

        public override void UpdateState()
        {
            _uiRound.UpdateTransition();
        }

        public override GameState.GameState GetNextState()
        {
            if (_uiRound.RoundCount-1 >= _data.RoundNumber)
                return GameState.GameState.GameEnd;
            if(_uiRound.Timer <=0)
                return GameState.GameState.RoundStart;
            return GameState.GameState.RoundTransition;
        }

        public override void OnTriggerEnter() {}
        public override void OnTriggerStay() {}
        public override void OnTriggerExit() {}
    }
}