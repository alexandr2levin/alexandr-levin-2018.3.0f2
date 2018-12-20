using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Game.Gameplay
{
    public class GameSession : MonoBehaviour
    {
        public int Score { private set; get; }

        public int TargetScore;
        public int TimeLeftSeconds;

        public float Progress
        {
            get
            {
                if (_timeTotalSeconds == -1) return 0f;
                return 1f - (float) TimeLeftSeconds / (float) _timeTotalSeconds;
            }
        }

        public int EfficiencyPercentages
        {
            get
            {
                var efficiency = (float) Score / (float) TargetScore;
                if (efficiency > 1f) efficiency = 1f;
                var percentages = Mathf.FloorToInt(efficiency * 100);
                return percentages;
            }
        }

        public event Action OnBegin;
        public event Action OnFinish;
        
        public event Action<float> OnTimerProgressChanged;
        public event Action<int> OnScoreChanged;

        private int _timeTotalSeconds = -1;
        private bool _sessionRunning = false;

        public void ChangeScore(int delta)
        {
            if (!_sessionRunning) return;
            Score += delta;
            OnScoreChanged?.Invoke(Score);
        }
        
        private void Start()
        {
            Application.targetFrameRate = 60;
            BeginSession();
        }

        private void BeginSession()
        {
            _sessionRunning = true;
            _timeTotalSeconds = TimeLeftSeconds;
            StartCoroutine(BeginSessionCoroutine());
        }
        
        private IEnumerator BeginSessionCoroutine()
        {
            // wait some time before starting the game
            // to let user understand the UI
            yield return new WaitForSeconds(3.0f);
            OnBegin?.Invoke();
            
            while (TimeLeftSeconds > 0)
            {
                yield return new WaitForSeconds(1.0f);
                TimeLeftSeconds -= 1;
                OnTimerProgressChanged?.Invoke(Progress);
            }
            OnTimerProgressChanged?.Invoke(Progress);
            FinishSession();
        }

        private void FinishSession()
        {
            _sessionRunning = false;
            OnFinish?.Invoke();
        }
    }
}