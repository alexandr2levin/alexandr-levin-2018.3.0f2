using Game.Base;
using UnityEngine.UI;

namespace Game.Gameplay.Overlay
{
    public class OverlayScreen : Screen<IOverlayView, OverlayPresenter>, IOverlayView
    {
        public int Score { get; set; }
        public int TimeLeftSeconds { get; set; }

        public Text TimerText;

        public Text ScoreText;
    }
}