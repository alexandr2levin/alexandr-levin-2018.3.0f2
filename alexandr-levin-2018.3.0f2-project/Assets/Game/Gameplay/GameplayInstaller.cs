using Game.Base;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class GameplayInstaller : MonoInstaller
    {

        public GameSession GameSession;
        public AudioPlayer AudioPlayer;
        
        public override void InstallBindings()
        {
            Container.BindInstance(GameSession);
            Container.BindInstance(AudioPlayer);
        }
    }
}