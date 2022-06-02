using Infrastructure.SceneManagement;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string MenuScene = "MenuScene";
        
        private IGameStateMachine _gameStateMachine;
        private ISceneLoader _sceneLoader;

        public BootstrapState(IGameStateMachine gameStateMachine, ISceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            _sceneLoader.LoadScene(MenuScene);
            _gameStateMachine.CastState<MenuState>();
        }

        public void Exit() { }
    }
}
