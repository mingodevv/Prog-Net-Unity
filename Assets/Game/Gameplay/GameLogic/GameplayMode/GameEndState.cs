using System.Collections.Generic;
using Game.GameState;
using UnityEngine.SceneManagement;

namespace Game.Gameplay.GameLogic
{
    public class GameEndState : BaseState<GameState.GameState>
    {
        private UIGameEnd _gameEndUI; 
        public GameEndState(UIManager uiManager) : base(GameState.GameState.GameEnd)
        {
            _gameEndUI= uiManager.GameEnd;
        }

        public override void EnterState()
        {
            _gameEndUI.UpdateUI(true);
        }

        public override void ExitState()
        {
            _gameEndUI.IsRestarting = false; 
            _gameEndUI.UpdateUI(false);
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