using Cysharp.Threading.Tasks;
using Infrastructure.SceneManagement;
using UnityEngine.SceneManagement;

namespace Infrastructure.States
{
    public class EndState : IState
    {
        private const string EndScene = "EndScene";
        
        private readonly IGameStateMachine _gameStateMachine;

        public EndState(IGameStateMachine gameStateMachine) => 
            _gameStateMachine = gameStateMachine;

        public void Enter() => Restart();

        private async void Restart()
        {
            await UniTask.WaitWhile(() => SceneManager.GetActiveScene().name != EndScene);
            
            _gameStateMachine.CastState<LoadLevelState>();
        }

        public void Exit() { }
    }
}