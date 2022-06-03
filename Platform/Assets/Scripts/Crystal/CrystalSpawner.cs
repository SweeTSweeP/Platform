using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Crystal
{
    public class CrystalSpawner : ICrystalSpawner
    {
        private const string CrystalAsset = "Crystal";
        private const string PlatformTag = "Platform";

        private const int MaxCountOfCrystals = 15;
        private const int StartCountOfCrystals = 10;

        private GameObject _crystal;
        private Transform _parent;

        private int _currentCrystalsCount;
        private bool _isWaitingToSpawn;
        private bool _isEnd;

        public void Initialize()
        {
            _isEnd = false;
            
            LoadCrystal();

            _parent = new GameObject("Crystals").transform;
        }

        public void TearDown()
        {
            _isEnd = true;

            if (_parent is null) return;
            
            foreach (Transform child in _parent)
            {
                Object.Destroy(child.gameObject);
            }
        }

        public void SpawnStarterCrystals()
        {
            for (var i = 0; i < StartCountOfCrystals; i++)
            {
                if (i == StartCountOfCrystals - 1)
                    TryToSpawnCrystal(true);
                else
                    TryToSpawnCrystal();
            }
        }

        private void SpawnAfterDestroy()
        {
            _currentCrystalsCount--;
            Debug.Log(_currentCrystalsCount);
            
            SpawnAfterCreation();
        }

        private async void SpawnAfterCreation()
        {
            if (_currentCrystalsCount >= MaxCountOfCrystals) return;
            if (_isWaitingToSpawn) return;

            _isWaitingToSpawn = true;

            await UniTask.Delay(TimeSpan.FromSeconds(7));

            if(_isEnd) return;

            TryToSpawnCrystal(true);
            
            Debug.Log(_currentCrystalsCount);

            _isWaitingToSpawn = false;
        }

        private void TryToSpawnCrystal(bool shouldSubscribe = false)
        {
            Vector3 position;
            List<Collider> overlappedObjects;

            SetPosition();

            while (overlappedObjects.Count > 0) SetPosition();

            var crystal = Object.Instantiate(_crystal, position, Quaternion.Euler(45, 45, 45), _parent);
            _currentCrystalsCount++;

            var crystalComponent = crystal.GetComponent<Crystal>();
            crystalComponent.CrystalDestroyed += SpawnAfterDestroy;

            if (!shouldSubscribe) return;

            crystalComponent.CrystalCreated += SpawnAfterCreation;
            
            void SetPosition()
            {
                var randomX = Random.Range(-9, 9);
                var randomZ = Random.Range(-9, 9);

                position = new Vector3(randomX, 1, randomZ);

                var hitColliders = new Collider[10];
                Physics.OverlapSphereNonAlloc(position, 1, hitColliders);
                overlappedObjects = hitColliders.Where(s => s != null && !s.CompareTag(PlatformTag)).ToList();
            }
        }

        private void LoadCrystal() => 
            _crystal = Addressables.LoadAssetAsync<GameObject>(CrystalAsset).WaitForCompletion();
    }
}
