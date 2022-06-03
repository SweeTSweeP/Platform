using System;
using Cysharp.Threading.Tasks;
using Infrastructure.UI;
using UnityEngine;
using Zenject;

namespace Player
{
    public class Health : MonoBehaviour
    {
        private const int ImmortalityTime = 3;
        
        private const string CrystalTag = "Crystal";
        private const string EnemyTag = "Enemy";

        private IUiHeroStatusMediator _heroStatusMediator;
        
        private event Action PlayerHealed;
        private event Action PlayerDamaged;
        private event Action PlayerDied;

        private int _currentHealth;
        private bool isImmortal;

        [Inject]
        public void Construct(IUiHeroStatusMediator heroStatusMediator)
        {
            _heroStatusMediator = heroStatusMediator;

            PlayerHealed += _heroStatusMediator.NotifyPlayerHealed;
            PlayerDamaged += _heroStatusMediator.NotifyPlayerDamaged;
            PlayerDied += _heroStatusMediator.NotifyPlayerDied;
        }

        private void Awake() => 
            _currentHealth = 3;


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(CrystalTag)) SelfHeal();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(isImmortal) return;

            if (collision.gameObject.CompareTag(EnemyTag))
            {
                if (_currentHealth > 0)
                {
                    DecreaseHealth();
                    Debug.Log($"Player health: {_currentHealth}");
                }

                if (_currentHealth == 0)
                {
                    Debug.Log("DEATH!");
                    Die();
                }
            }
        }

        private void OnDestroy()
        {
            PlayerHealed -= _heroStatusMediator.NotifyPlayerHealed;
            PlayerDamaged -= _heroStatusMediator.NotifyPlayerDamaged;
            PlayerDied -= _heroStatusMediator.NotifyPlayerDied;
        }

        private void Die() => 
            PlayerDied?.Invoke();

        private void DecreaseHealth()
        {
            _currentHealth--;
            PlayerDamaged?.Invoke();
            BecameImmortal();

            async void BecameImmortal()
            {
                isImmortal = true;

                await UniTask.Delay(TimeSpan.FromSeconds(ImmortalityTime));

                isImmortal = false;
            }
        }

        private void SelfHeal()
        {
            if (_currentHealth < 3) _currentHealth++;

            Debug.Log($"Player health: {_currentHealth}");
            PlayerHealed?.Invoke();
        }
    }
}
