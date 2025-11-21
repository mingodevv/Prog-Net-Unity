using System.Collections.Generic;
using Game.GameState;
using UnityEngine.SceneManagement;

namespace Game.Gameplay.GameLogic
{
    public class GameEndState : BaseState<GameState.GameState>
    {
        private UIGameEnd _gameEndUI; 
        public GameEndState(UIGameEnd gameEndUi) : base(GameState.GameState.GameEnd)
        {
            _gameEndUI = gameEndUi;
        }

        public override void EnterState()
        {
            _gameEndUI.gameObject.SetActive(true);
        }

        public override void ExitState()
        {
            _gameEndUI.IsRestarting = false; 
            _gameEndUI.gameObject.SetActive(false);
        }

        public override void UpdateState()
        {
        }

        public override GameState.GameState GetNextState()
        {
            if (_gameEndUI.IsRestarting)
            {
                
                SceneManager.LoadScene("Game/MainMenu/MainMenuScene");
                return GameState.GameState.GameStart; 
            }
            return GameState.GameState.GameEnd;
        }

        public override void OnTriggerEnter() {}
        public override void OnTriggerStay() {}
        public override void OnTriggerExit() {}
    }
}