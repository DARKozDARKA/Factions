using UnityEngine;

namespace CodeBase.Infastructure
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private GameStateMachine _gameStateMachine;
        private SceneLoader _sceneLoader;
        private IPrefabGameFactory _factory;
        private readonly LoadingCurtain _curtain;
        private readonly IAssetProvider _assetProvider;
        private readonly IPrefabGameFactory _prefabGameFactory;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain curtain,
            IAssetProvider assetProvider, IPrefabGameFactory prefabGameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _assetProvider = assetProvider;
            _prefabGameFactory = prefabGameFactory;
        }

        public void Enter(string name)
        {
            _curtain.Show();
            _assetProvider.Cleanup();
            _prefabGameFactory.WarmUp();
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