using Crystal;
using Cysharp.Threading.Tasks;
using Infrastructure.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Infrastructure.States
{
    public class LoadLevelState : IState
    {
        private const string MainScene = "MainScene";
        private const string EndScene = "EndScene";
        
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

            SpawnCrystals();
            
            _gameStateMachine.CastState<GameLoopState>();
        }

        public void Exit() { }

        private async void SpawnCrystals()
        {
            await UniTask.WaitWhile(() => SceneManager.GetActiveScene().name != MainScene);
            
            _crystalSpawner.Initialize();
            _crystalSpawner.SpawnStarterCrystals();
            AddListener();
        }

        private void AddListener()
        {
            var restartButton = GameObject.FindWithTag("RestartButton");
            restartButton.GetComponentInChildren<Button>()
                .onClick
                .AddListener(() =>
                {
                    _sceneLoader.LoadScene(EndScene);
                    _gameStateMachine.CastState<EndState>();
                });

            restartButton.transform.parent.gameObject.SetActive(false);
        }
    }
}