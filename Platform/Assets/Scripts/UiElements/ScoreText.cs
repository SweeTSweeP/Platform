using Infrastructure.UI;
using TMPro;
using UnityEngine;
using Zenject;

namespace UiElements
{
    [RequireComponent(typeof(TMP_Text))]
    public class ScoreText : MonoBehaviour
    {
        private const string ScoreKey = "Score";
        
        [SerializeField] private int crystalValue;
        [SerializeField] private TMP_Text bestScoreText;
        [SerializeField] private GameObject restartForm;

        [Inject] private IUiHeroStatusMediator _heroStatusMediator;
        private TMP_Text _scoreText;
        
        private int _currentScore;

        [Inject]
        private void Construct(IUiHeroStatusMediator heroStatusMediator) => 
            _heroStatusMediator = heroStatusMediator;

        private void Start()
        {
            _currentScore = 0;
            _scoreText = GetComponent<TMP_Text>();
            _scoreText.text = $"Score: {_currentScore}";

            _heroStatusMediator.PlayerHealed += IncreaseScore;
            _heroStatusMediator.PlayerDied += EndGame;
        }

        private void IncreaseScore()
        {
            _currentScore += crystalValue;
            _scoreText.text = $"Score: {_currentScore}";
        }
        
        private void EndGame()
        {
            SaveScore();
            restartForm.SetActive(true);
        }

        private void SaveScore()
        {
            var score = PlayerPrefs.GetInt(ScoreKey);

            if (_currentScore <= score) return;
            PlayerPrefs.SetInt(ScoreKey, _currentScore);
            PlayerPrefs.Save();

            bestScoreText.text = $"Best Result: {PlayerPrefs.GetInt(ScoreKey)}";
        }

        private void OnDestroy() => 
            _heroStatusMediator.PlayerHealed -= IncreaseScore;
    }
}
