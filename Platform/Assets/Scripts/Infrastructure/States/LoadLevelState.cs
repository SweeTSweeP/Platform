using Infrastructure.SceneManagement;

namespace Infrastructure.States
{
    public class LoadLevelState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;

        public LoadLevelState(IGameStateMachine gameStateMachine, ISceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            _sceneLoader.LoadScene("MainScene");

            _gameStateMachine.CastState<GameLoopState>();
        }

        public void Exit() { }
    }
}