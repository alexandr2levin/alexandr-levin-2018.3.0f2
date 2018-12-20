using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Screen = Game.Base.Screen;

namespace Game.Gameplay.Overlay
{
    public class OverlayScreen : Screen
    {

        [Header("Overlay")]
        public Text TimerText;
        public Text ScoreText;

        [Header("Finish Panel")]
        public Text FinalScoreText;
        public Text EfficiencyText;
        public Button DoneButton;
        public Color BadColor;
        public Color NormalColor;
        public Color GoodColor;

        private Router _router;
        private GameSession _session;
        private Animator _animator;

        [Inject]
        private void InjectDependencies(Router router, GameSession gameSession)
        {
            _router = router;
            _session = gameSession;
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            
            _session.OnTimerProgressChanged += _ => UpdateTimerText(_session.TimeLeftSeconds);
            _session.OnScoreChanged += UpdateScoreText;
            _session.OnFinish += () => SetFinishState();
            
            DoneButton.onClick.AddListener(() =>
            {
                if (_session.EfficiencyPercentages == 100)
                {
                    // С:<
                    Application.OpenURL("https://spb.hh.ru/resume/b7302605ff059d71210039ed1f553357686745");
                }
                else
                {
                    StartTransitionToRestart();
                }
            });
        }

        private void Start()
        {
            UpdateTimerText(_session.TimeLeftSeconds);
            UpdateScoreText(_session.Score);
        }

        private void UpdateTimerText(int secondsLeft)
        {
            TimerText.text = secondsLeft.ToString();
        }

        private void UpdateScoreText(int score)
        {
            ScoreText.text = score.ToString();
        }

        private void SetFinishState()
        {
            FinalScoreText.text = _session.Score.ToString();
            var effeciency = _session.EfficiencyPercentages;
            EfficiencyText.text = effeciency.ToString() + "%";
            if (effeciency <= 70)
            {
                EfficiencyText.color = BadColor;
            } else if (effeciency <= 99)
            {
                EfficiencyText.color = NormalColor;
            } else if (effeciency == 100)
            {
                EfficiencyText.color = GoodColor;
                DoneButton.GetComponent<Image>().color = GoodColor;
                DoneButton.GetComponentInChildren<Text>().text = "ОТПРАВИТЬ ОФФЕР";
            }
            _animator.SetTrigger("finish");
        }

        private void StartTransitionToRestart()
        {
            _animator.SetTrigger("fade_out");
        }

        // called from Animation by Animation Event
        private void FinishTransitionToRestart()
        {
            _router.ResetCurrentScene();
        }
        
    }
}