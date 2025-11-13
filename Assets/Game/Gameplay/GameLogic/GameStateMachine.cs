namespace Game.Gameplay.GameLogic
{
    public class GameStateMachine : StateManager<GameStateMachine.GameState>
    {
        public enum GameState
        {
            GameStart, 
            RoundStart,
            EndRound, 
            TransitionRound, 
            GameEnd
        }

        void Awake()
        {
            CurrentState = States[GameState.GameStart];
        }
    }
}