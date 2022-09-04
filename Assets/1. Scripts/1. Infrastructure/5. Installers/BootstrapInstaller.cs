using CodeBase.Infastructure;
using CodeBase.TerrainGenerator;
using UnityEngine;
using Zenject;
public class BootstrapInstaller : MonoInstaller
{
    [SerializeField] private LoadingCurtain _curtain;
    [SerializeField] private CoroutineRunner _coroutineRunner;
    private SceneLoader _sceneLoader;

    public override void InstallBindings()
    {
        RegisterServices();
    }

    public override void Start()
    {
        base.Start();
        Container.Resolve<GameStateMachine>().Enter<BootstrapState>();
    }

    private void RegisterServices()
    {
        RegisterUtilities();
        RegisterFactories();
        RegisterGenerators();
        RegisterDebuggers();

        Container.Bind<GameStateMachine>().AsSingle().NonLazy();
    }

    private void RegisterUtilities()
    {
        Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
        Container.Bind<LoadingCurtain>().FromInstance(Instantiate(_curtain)).AsSingle();
        Container.Bind<ICoroutineRunner>().To<CoroutineRunner>().FromInstance(Instantiate(_coroutineRunner)).AsSingle();
        Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
        Container.Bind<SceneLoader>().AsSingle();
        Container.Bind<BlockTextureAtlas>().AsSingle();
        Container.Bind<MapProvider>().AsSingle();
    }

    private void RegisterFactories()
    {
        Container.Bind<IPrefabGameFactory>().To<PrefabGameFactory>().AsSingle();
        Container.Bind<INatureGameFactory>().To<NatureGameFactory>().AsSingle();
    }


    private void RegisterGenerators()
    {
        Container.Bind<ITerrainGenerator>().To<TerrainGenerator>().AsSingle();
        Container.Bind<ILayersGenerator>().To<LayersGenerator>().AsSingle();
    }

    private void RegisterDebuggers()
    {
        if (!Application.isEditor) return;
        
        Container.Bind<DebugProvider>().AsSingle().NonLazy();
    }
}