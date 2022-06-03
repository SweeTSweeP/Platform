using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        private GameObject enemyAsset;

        private void Start()
        {
            LoadEnemy();
            SpawnEnemy();
        }

        public void SpawnEnemy() => 
            Instantiate(enemyAsset, transform.position, Quaternion.identity, transform);

        private void LoadEnemy() => 
            enemyAsset = Addressables.LoadAssetAsync<GameObject>("Enemy").WaitForCompletion();
    }
}