using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Crystal
{
    public class Crystal : MonoBehaviour
    {
        private const string PlayerTag = "Player";

        public event Action CrystalCreated;
        public event Action CrystalDestroyed;

        private void Start() => 
            InvokeAfterCreation();

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag(PlayerTag)) return;
            
            CrystalDestroyed?.Invoke();
            Destroy(gameObject);
        }

        private async void InvokeAfterCreation()
        {
            await UniTask.DelayFrame(20);
            
            CrystalCreated?.Invoke();
        }
    }
}
