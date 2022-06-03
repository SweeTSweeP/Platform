using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviour
    {
        private const string SpawnPointTag = "SpawnPoint";

        [SerializeField] private float enemySpeed;
        
        private Transform[] _points;
        private int _destPoint;
        private NavMeshAgent _agent;

        private void Start ()
        {
           InitializePoints();
            
            _agent = GetComponent<NavMeshAgent>();
            _agent.autoBraking = false;
            _agent.speed = enemySpeed;

            GotoNextPoint();
        }

        private void Update () 
        {
            if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
                GotoNextPoint();
        }

        private void InitializePoints()
        {
            _points = GameObject
                .FindGameObjectsWithTag(SpawnPointTag)
                .Select(s => s.transform)
                .OrderBy(s => Random.value)
                .ToArray();
        }

        private void GotoNextPoint() 
        {
            if (_points.Length == 0)
                return;
            
            _agent.destination = _points[_destPoint].position;
            _destPoint = (_destPoint + 1) % _points.Length;
        }
    }
}