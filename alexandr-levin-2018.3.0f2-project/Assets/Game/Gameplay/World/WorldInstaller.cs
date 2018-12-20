using Game.Gameplay.World;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class WorldInstaller : MonoInstaller
    {
        
        public Transform WorldTransform;
        public GameObject BallPrefab;
        
        public override void InstallBindings()
        {
            Container.BindMemoryPool<Ball, Ball.Pool>()
                .WithInitialSize(10)
                .FromComponentInNewPrefab(BallPrefab)
                .UnderTransform(WorldTransform);
        }
    }
}