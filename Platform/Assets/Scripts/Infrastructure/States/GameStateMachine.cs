using System;
using System.Collections.Generic;
using Crystal;
using Infrastructure.SceneManagement;

namespace Infrastructure.States
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IState> _states;

        private IState _activeState;

        public GameStateMachine(ISceneLoader sceneLoader, ICrystalSpawner crystalSpawner)
        {
            _states = new Dictionary<Type, IState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader),
                [typeof(MenuState)] = new MenuState(this),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, crystalSpawner),
                [typeof(GameLoopState)] = new GameLoopState()
            };
        }

        public void CastState<T>() where T : class, IState
        {
            var state = ChangeState<T>();
            state.Enter();
        }

        private T ChangeState<T>() where T : class, IState
        {
            _activeState?.Exit();

            var state = GetState<T>(typeof(T));
            _activeState = state;

            return state;
        }

        private T GetState<T>(Type stateType) where T : class, IState => 
            _states[stateType] as T;
    }
}