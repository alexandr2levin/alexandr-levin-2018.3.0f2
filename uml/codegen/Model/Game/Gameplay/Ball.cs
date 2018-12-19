
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Gameplay{
    public class Ball {

        public Ball() {
        }

        public Int Size;

        public Int Price;

        public Int Speed;

        public Int BaseSpeed;

        public GameSession _session;



        public void Initialize(BallArgs args) {
            // TODO implement here
        }

        private void UpdateBaseSpeedByTimeLeft(Int timeLeft) {
            // TODO implement here
        }

        private void Explode() {
            // TODO implement here
        }

    }
}