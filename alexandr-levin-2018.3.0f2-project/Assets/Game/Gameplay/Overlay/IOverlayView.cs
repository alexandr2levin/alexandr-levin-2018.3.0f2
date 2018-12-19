using Game.Base;

namespace Game.Gameplay.Overlay
{
    public interface IOverlayView : IView
    {
        int Score { get; set; }
        int TimeLeftSeconds { get; set; }
    }
}