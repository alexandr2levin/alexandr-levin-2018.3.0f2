using Game.Gameplay.World.Ball;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.World
{
    public class WorldInstaller : MonoInstaller
    {
        
        public Transform WorldTransform;
        public GameObject BallPrefab;
        
        public override void InstallBindings()
        {
            Container.BindMemoryPool<BallController, BallController.Pool>()
                .WithInitialSize(10)
                .FromComponentInNewPrefab(BallPrefab)
                .UnderTransform(WorldTransform);
        }
    }
}