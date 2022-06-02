using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Move : MonoBehaviour
    {
        private NavMeshAgent _agent;

        private void Start() => _agent = GetComponent<NavMeshAgent>();

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0)) return;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hit, 100))
                _agent.destination = hit.point;
        }
    }
}
