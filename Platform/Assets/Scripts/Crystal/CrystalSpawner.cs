using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Random = UnityEngine.Random;

namespace Crystal
{
    public class CrystalSpawner : MonoBehaviour
    {
        private const string CrystalAsset = "Crystal";
        private const string PlatformTag = "Platform";

        private const int MaxCountOfCrystals = 15;

        private GameObject _crystal;

        private void Start()
        {
            LoadCrystal();
            SpawnStarterCrystals();
        }

        private void SpawnStarterCrystals()
        {
            for (var i = 0; i < 10; i++)
            {
                TryToSpawnCrystal();
            }
        }

        private void TryToSpawnCrystal()
        {
            Vector3 position;
            List<Collider> overlappedObjects; 
            
            SetPosition();

            while (overlappedObjects.Count > 0) SetPosition();

            Instantiate(_crystal, position, Quaternion.Euler(45,45,45), transform);

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
