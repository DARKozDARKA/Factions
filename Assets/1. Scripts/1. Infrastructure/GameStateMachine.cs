using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;
using CodeBase.TerrainGenerator;

namespace CodeBase.Infastructure
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _currentState;

        private readonly SceneLoader _sceneLoader;
        private readonly DiContainer _container;

        public GameStateMachine(DiContainer container)
        {
            _container = container;
            _sceneLoader = _container.Resolve<SceneLoader>();
            LoadingCurtain curtain = _container.Resolve<LoadingCurtain>();

            _states = new Dictionary<Type, IExitableState>()
            {
                {typeof(BootstrapState), new BootstrapState(this, _sceneLoader)},
                {typeof(LoadLevelState), new LoadLevelState(this, _sceneLoader, curtain)},
                {typeof(GenerateTerrainState), new GenerateTerrainState(this, container.Resolve<ITerrainGenerator>(), curtain, container.Resolve<MapProvider>(), 
                    container.Resolve<DebugProvider>(), container.Resolve<ILayersGenerator>())},
                {typeof(GameLoopState), new GameLoopState(this, _sceneLoader)}
            };

        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = LoadState<TState>();
            state.Enter();
        }
        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = LoadState<TState>();
            state.Enter(payload);
        }

        private TState LoadState<TState>() where TState : class, IExitableState
        {
            _currentState?.Exit();
            TState state = GetState<TState>();
            _currentState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}


