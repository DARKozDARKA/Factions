using UnityEngine;

namespace CodeBase.Infastructure
{
    public class LoadLevelState : IPayloadedState<string>
    {

        private GameStateMachine _gameStateMachine;
        private SceneLoader _sceneLoader;
        private IPrefabGameFactory _factory;
        private readonly LoadingCurtain _curtain;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain curtain)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            //_factory = factory;
        }

        public void Enter(string name)
        {
            _curtain.Show();
            _sceneLoader.Load(name, action: OnLoaded);
        }

        public void OnLoaded()
        {
            _gameStateMachine.Enter<GenerateTerrainState>();
        }


        public void Exit()
        {

        }
    }
}