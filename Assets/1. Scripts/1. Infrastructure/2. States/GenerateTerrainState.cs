using CodeBase.TerrainGenerator;

namespace CodeBase.Infastructure
{
    public class GenerateTerrainState : IState
    {
        private ITerrainGenerator _generator;
        private LoadingCurtain _curtain;
        private MapProvider _provider;
        private GameStateMachine _gameStateMachine;
        private DebugProvider _debugProvider;
        private readonly ILayersGenerator _layersGenerator;
        private readonly IPrefabGameFactory _gameFactory;
        private readonly INatureGameFactory _natureGameFactory;

        public GenerateTerrainState(GameStateMachine gameStateMachine, ITerrainGenerator generator, LoadingCurtain curtain, MapProvider provider,
        DebugProvider debugProvider, ILayersGenerator layersGenerator, IPrefabGameFactory gameFactory, INatureGameFactory natureGameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _generator = generator;
            _curtain = curtain;
            _provider = provider;
            _debugProvider = debugProvider;
            _layersGenerator = layersGenerator;
            _gameFactory = gameFactory;
            _natureGameFactory = natureGameFactory;
        }
        public void Enter()
        {
            _generator.OnLoaded += OnLoaded;
            _generator.GenerateTerrain();
        }

        public void Exit()
        {
            _curtain.Hide();
        }

        private async void OnLoaded()
        {
            _generator.OnLoaded -= OnLoaded;
            _provider.SetMap(_generator.GetTerrainMap());
            
            _natureGameFactory.CreateNatureRoot();
            
            _layersGenerator.GenerateLayers();
            
            new NavMeshSurfaceBaker().GenerateNavMesh(_gameFactory);
            
            var drawer = await _debugProvider.CreateDebugObject();
            drawer.SetMap();

            _gameStateMachine.Enter<GameLoopState>();
        }
    }
}