using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Gameplay.World.Ball;
using Game.Utils;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Gameplay.World
{
    public class WorldController : MonoBehaviour
    {

        public Color[] BallColors;
        
        private BallController.Pool _pool;
        private List<BallController> _activeBalls = new List<BallController>();

        private Bounds _worldBounds;
        private bool _resumed = false;

        private GameSession _gameSession;

        private void Awake()
        {
            _worldBounds = Camera.main.OrthographicBounds();
            _gameSession.OnBegin += ResumeSpawning;
            _gameSession.OnFinish += PauseSpawning;
        }

        [Inject]
        public void InjectDependencies(BallController.Pool pool, GameSession gameSession)
        {
            _pool = pool;
            _pool.InactiveItems.ToList()
                .ForEach(ball => { ball.OnExploded += DespawnBall; });

            _gameSession = gameSession;
        }
        
        public void SpawnBall()
        {
            var ballSize = Random.Range(0.5f, 1.5f);
            var price = 2;
            if (ballSize >= 0.5f && ballSize <= 0.8f) price += 1;
            if (ballSize > 0.8f && ballSize <= 1.2f) { /* do nothing */ }
            if (ballSize > 1.2f && ballSize <= 1.5f) price -= 1;

            if (BallColors.Length == 0)
            {
                throw new InvalidOperationException("There is no BallColors specified on WorldController");
            }

            var color = BallColors[Random.Range(0, BallColors.Length - 1)]; // Random.ColorHSV() also works
            
            var ball = _pool.Spawn(new BallController.Args(ballSize, price, color));

            var worldEdgesOffset = ball.Bounds.size.x / 2;
            var xPos = Random.Range(_worldBounds.min.x + worldEdgesOffset, _worldBounds.max.x - worldEdgesOffset);
            
            var position = new Vector3(xPos, _worldBounds.min.y - (ball.Bounds.size.y / 2));
            ball.transform.localPosition = position;
            
            _activeBalls.Add(ball);
        }

        public void FreeExtraBalls()
        {
            _activeBalls
                .Where(ball => ball.Bounds.min.y > _worldBounds.max.y)
                .ToList()
                .ForEach(DespawnBall);
        }

        private void DespawnBall(BallController ball)
        {
            _pool.Despawn(ball);
            _activeBalls.Remove(ball);
        }

        private void Update()
        {
            FreeExtraBalls();
        }

        private void ResumeSpawning()
        {
            _resumed = true;
            StartCoroutine(SpawnLoopCoroutine());
        }

        private IEnumerator SpawnLoopCoroutine()
        {
            while (_resumed)
            {
                SpawnBall();
                yield return new WaitForSeconds(0.8f);
            }
        }

        private void PauseSpawning()
        {
            _resumed = false;
            _activeBalls.ForEach(ball => ball.ForceExplode());
        }
        
    }
}