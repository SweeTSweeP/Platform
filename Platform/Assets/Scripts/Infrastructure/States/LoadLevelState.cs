using Crystal;
using Cysharp.Threading.Tasks;
using Infrastructure.SceneManagement;
using UnityEngine.SceneManagement;

namespace Infrastructure.States
{
    public class LoadLevelState : IState
    {
        private const string MainScene = "MainScene";
        
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly ICrystalSpawner _crystalSpawner;

        public LoadLevelState(
            IGameStateMachine gameStateMachine, 
            ISceneLoader sceneLoader,
            ICrystalSpawner crystalSpawner)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _crystalSpawner = crystalSpawner;
        }

        public void Enter()
        {
            _sceneLoader.LoadScene(MainScene);
            _crystalSpawner.Initialize();
            
            SpawnCrystals();
            
            _gameStateMachine.CastState<GameLoopState>();
        }

        public void Exit() { }

        private async void SpawnCrystals()
        {
            await UniTask.WaitWhile(() => SceneManager.GetActiveScene().name != MainScene);
            
            _crystalSpawner.SpawnStarterCrystals();
        }
    }
}