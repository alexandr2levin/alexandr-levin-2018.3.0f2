using Game.Base;
using Zenject;

namespace Game.Menu
{
    public class MenuInstaller : MonoInstaller
    {

        public AudioPlayer AudioPlayer;
        public MenuAudioController MenuAudioController;
        
        public override void InstallBindings()
        {
            Container.BindInstance(AudioPlayer);
            Container.BindInstance(MenuAudioController);
        }
    }
}