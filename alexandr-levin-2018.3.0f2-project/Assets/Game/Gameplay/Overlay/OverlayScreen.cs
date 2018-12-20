using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Screen = Game.Base.Screen;

namespace Game.Gameplay.Overlay
{
    public class OverlayScreen : Screen
    {
        public int Score { get; set; }
        public int TimeLeftSeconds { get; set; }

        public Text TimerText;
        public Text ScoreText;

        public Button MenuButton;

        private Router _router;
        private GameSession _gameSession;
        private Animator _animator;

        [Inject]
        private void InjectDependencies(Router router, GameSession gameSession)
        {
            _router = router;
            _gameSession = gameSession;
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            
            _gameSession.OnTimerProgressChanged += _ => UpdateTimerText(_gameSession.TimeLeftSeconds);
            _gameSession.OnScoreChanged += UpdateScoreText;
            _gameSession.OnFinish += () => _animator.SetTrigger("Finish");
            
            MenuButton.onClick.AddListener(() => { _router.ToMenu(); });
        }

        private void Start()
        {
            UpdateTimerText(_gameSession.TimeLeftSeconds);
            UpdateScoreText(_gameSession.Score);
        }

        private void UpdateTimerText(int secondsLeft)
        {
            TimerText.text = secondsLeft.ToString();
        }

        private void UpdateScoreText(int score)
        {
            ScoreText.text = score.ToString();
        }
        
    }
}