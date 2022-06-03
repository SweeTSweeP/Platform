using System;
using Cysharp.Threading.Tasks;
using Infrastructure.UI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private float timeToSpawn = 10;
        
        private const int MaxEnemyOnSpawner = 2;
        private const string EnemyKey = "Enemy";
        
        private IUiHeroStatusMediator _heroStatusMediator;

        private GameObject _enemyAsset;
        private bool _isSpawning;
        private bool _isEnd;

        [Inject]
        public void Construct(IUiHeroStatusMediator heroStatusMediator) => 
            _heroStatusMediator = heroStatusMediator;

        private void Start()
        {
            _isEnd = false;
            
            LoadEnemy();
            SpawnEnemy();

            _heroStatusMediator.PlayerDied += StopSpawn;
        }

        private void Update()
        {
            if (_isEnd || _isSpawning || transform.childCount >= MaxEnemyOnSpawner) return;

            WaitAndSpawn();
        }

        private void StopSpawn()
        {
            _isEnd = true;
            
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            
            gameObject.SetActive(false);
        }

        private async void WaitAndSpawn()
        {
            _isSpawning = true;

            await UniTask.Delay(TimeSpan.FromSeconds(timeToSpawn));
            
            SpawnEnemy();

            _isSpawning = false;
        }

        private void SpawnEnemy() => 
            Instantiate(_enemyAsset, transform.position, Quaternion.identity, transform);

        private void LoadEnemy() => 
            _enemyAsset = Addressables.LoadAssetAsync<GameObject>(EnemyKey).WaitForCompletion();
    }
}