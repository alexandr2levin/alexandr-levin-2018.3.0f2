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
        public int Score;

        public int TimeLeftSeconds;

        public event Action<int> OnTimeLeftChanged;

        private bool _sessionRunning = false;

        public void ChangeScore(int delta)
        {
            Score += delta;
        }
        
        private void Start()
        {
            StartSession();
        }

        private void StartSession()
        {
            _sessionRunning = true;
            StartCoroutine(StartCountdown());
        }
        
        private IEnumerator StartCountdown()
        {
            while (TimeLeftSeconds > 0)
            {
                yield return new WaitForSeconds(1.0f);
                TimeLeftSeconds -= 1;
                OnTimeLeftChanged?.Invoke(TimeLeftSeconds);
            }
            
            OnTimeLeftChanged?.Invoke(0);
            FinishSession();
        }

        private void FinishSession()
        {
            _sessionRunning = false;
        }
    }
}