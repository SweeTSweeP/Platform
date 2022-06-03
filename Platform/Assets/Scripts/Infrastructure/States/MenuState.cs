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
        private const string MenuScene = "ManuScene";

        private readonly IGameStateMachine _gameStateMachine;

        public MenuState(IGameStateMachine gameStateMachine) => 
            _gameStateMachine = gameStateMachine;

        public void Enter() => 
            AddListener();

        public void Exit() { }

        private async void AddListener()
        {
            await UniTask.WaitUntil(() => SceneManager.GetActiveScene().name != MenuScene);
            
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