using Cysharp.Threading.Tasks;
using Infrastructure.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

namespace Infrastructure.States
{
    public class MenuState : IState
    {
        private const string StartButtonTag = "StartButton";
        private const string MainScene = "MainScene";

        private readonly IGameStateMachine _gameStateMachine;

        public MenuState(IGameStateMachine gameStateMachine) => 
            _gameStateMachine = gameStateMachine;

        public void Enter() => 
            AddListener();

        public void Exit() { }

        private async void AddListener()
        {
            await UniTask.WaitUntil(() => SceneManager.GetActiveScene().name != MainScene);
            
            GameObject
                .FindWithTag(StartButtonTag)
                .GetComponent<Button>()
                .onClick
                .AddListener(() =>
                {
                    _gameStateMachine.CastState<LoadLevelState>();
                });
        }
    }
}