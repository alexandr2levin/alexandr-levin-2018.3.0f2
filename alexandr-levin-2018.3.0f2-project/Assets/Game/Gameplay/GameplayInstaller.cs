using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class GameplayInstaller : MonoInstaller
    {

        public GameSession GameSession;
        
        public override void InstallBindings()
        {
            Container.BindInstance(GameSession);
        }
    }
}