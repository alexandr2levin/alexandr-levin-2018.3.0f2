
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Gameplay{
    public class GameSession {

        public GameSession() {
        }

        public int Score;

        public int TimeLeftSeconds;

        public Action<int> OnTimeLeftChanged;


        public void Reset() {
            // TODO implement here
        }

        public void ChangeScore(Int delta) {
            // TODO implement here
        }

    }
}