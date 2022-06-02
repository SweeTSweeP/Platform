using System;
using Infrastructure.States;
using UnityEngine;
using Zenject;

namespace Infrastructure.Bootstrap
{
    public class Bootstrapper : MonoBehaviour
    {
        private IGameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(IGameStateMachine gameStateMachine) => 
            _gameStateMachine = gameStateMachine;

        private void Awake()
        {
            _gameStateMachine.CastState<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}