using Game.Base;

namespace Game.Gameplay.Overlay
{
    public class OverlayPresenter : Presenter<IOverlayView>
    {
        public OverlayPresenter()
        {
        }

        private GameSession _session;
    }
}