using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    [RequireComponent(typeof(Animator), typeof(NavMeshAgent))]
    public class HeroAnimate : MonoBehaviour
    {
        private static readonly int IsRun = Animator.StringToHash("IsRun");
        
        private Animator _animator;
        private NavMeshAgent _navMeshAgent;

        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
        }

        private void Update() => 
            _animator.SetBool(IsRun, _navMeshAgent.velocity.magnitude != 0);
    }
}