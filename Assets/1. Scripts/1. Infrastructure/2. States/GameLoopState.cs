namespace CodeBase.Infastructure
{
    public class GameLoopState : IState
    {
        private GameStateMachine _gameStateMachine;
        private SceneLoader _sceneLoader;
        public GameLoopState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            //throw new System.NotImplementedException();
        }

        void IExitableState.Exit()
        {
            //throw new System.NotImplementedException();
        }
    }
}