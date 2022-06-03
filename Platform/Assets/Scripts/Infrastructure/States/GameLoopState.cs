using Crystal;
using Infrastructure.SceneManagement;
using UnityEngine;

namespace Infrastructure.States
{
    public class GameLoopState : IState
    {
        private const string EndScene = "EndScene";
        
        private readonly ICrystalSpawner _crystalSpawner;

        public GameLoopState(
            ICrystalSpawner crystalSpawner)
        {
            _crystalSpawner = crystalSpawner;
        }

        public void Enter() { }

        public void Exit() => 
            _crystalSpawner.TearDown();
    }
}