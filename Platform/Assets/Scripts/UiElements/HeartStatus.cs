using System;
using Infrastructure.UI;
using UnityEngine;
using Zenject;

namespace UiElements
{
    public class HeartStatus : MonoBehaviour
    {
        [SerializeField] private GameObject[] hearts;

        private IUiHeroStatusMediator _heroStatusMediator;
        
        [Inject]
        public void Construct(IUiHeroStatusMediator heroStatusMediator) => 
            _heroStatusMediator = heroStatusMediator;

        private void Start()
        {
            _heroStatusMediator.PlayerHealed += AddHeart;
            _heroStatusMediator.PlayerDamaged += RemoveHearth;
        }

        private void AddHeart()
        {
            foreach (var heart in hearts)
            {
                if (heart.activeSelf) continue;
                heart.SetActive(true);
                return;
            }
        }

        private void RemoveHearth()
        {
            for (var i = hearts.Length - 1; i >= 0; i--)
            {
                if(!hearts[i].activeSelf) continue;
                hearts[i].SetActive(false);
                return;
            }
        }

        private void OnDestroy()
        {
            _heroStatusMediator.PlayerHealed -= AddHeart;
            _heroStatusMediator.PlayerDamaged -= RemoveHearth;
        }
    }
}
