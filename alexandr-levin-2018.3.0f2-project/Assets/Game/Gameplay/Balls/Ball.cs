using UnityEngine;

namespace Game.Gameplay.Balls
{
    public class Ball : MonoBehaviour
    {
        
        public int Size;
        public int Price;
        public int Speed;
        public int BaseSpeed;

        private GameSession _session;

        public void Initialize(BallArgs args)
        {
            // TODO implement here
        }

        private void UpdateBaseSpeedByTimeLeft(int timeLeft)
        {
            // TODO implement here
        }

        private void Explode()
        {
            // TODO implement here
        }
    }
}