using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Game.Gameplay.World
{
    [RequireComponent(typeof(Animator))]
    public class Ball : MonoBehaviour, IPointerClickHandler
    {

        public event Action<Ball> OnExploded;
        public Bounds Bounds => _renderer.bounds;

        public float Size;
        public int Price;
        [Space(5)]
        public float BaseSpeed;
        [Space(10)] 
        public SpriteRenderer TailSpriteRenderer;

        private float _initialBaseSpeed;

        private GameSession _session;
        private Animator _animator;
        private SpriteRenderer _renderer;

        private bool _isDead;

        [Inject]
        private void InjectDependencies(GameSession session)
        {
            _session = session;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            StartExplode();
        }

        public void ForceExplode()
        {
            StartExplode();
        }
        
        private void Update()
        {
            if (!_isDead)
            {
                var sizeFactor = Size * 0.8f;
                var movementVector = (Vector3.up * BaseSpeed) + (Vector3.up / sizeFactor);
                transform.position += movementVector * Time.deltaTime;
            }
        }
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _renderer = GetComponent<SpriteRenderer>();
            _initialBaseSpeed = BaseSpeed;
        }
        
        private void OnSpawned(Args args)
        {
            _isDead = false;
            ApplyArgs(args);
            _session.OnTimerProgressChanged += UpdateBaseSpeedByTimerProgress;
            UpdateBaseSpeedByTimerProgress(_session.Progress);
        }
        
        private void OnDespawned()
        {
            _session.OnTimerProgressChanged -= UpdateBaseSpeedByTimerProgress;
            _animator.SetTrigger("rise_again");
        }

        private void ApplyArgs(Args args)
        {
            if (args.Size <= 0)
            {
                throw new ArgumentException("Args.size must be bigger than 0");
            }
            Size = args.Size;
            Price = args.Price;
            
            transform.localScale = new Vector3(Size, Size, Size);
            _renderer.color = args.Color;
            TailSpriteRenderer.color = args.Color;
        }

        private void UpdateBaseSpeedByTimerProgress(float progress)
        {
            BaseSpeed = _initialBaseSpeed + (_initialBaseSpeed * progress * 1.75f);
        }

        private void StartExplode()
        {
            if (_isDead) return;
            _isDead = true;
            _animator.SetTrigger("explode");
            _session.ChangeScore(Price);
        }

        // called from Animation by Animation Event
        private void FinishExplode()
        {
            OnExploded?.Invoke(this);
        }
        
        public class Pool : MonoMemoryPool<Args, Ball>
        {
            protected override void Reinitialize(Args args, Ball ball)
            {
                ball.OnSpawned(args);
            }

            protected override void OnDespawned(Ball ball)
            {
                ball.OnDespawned();
                base.OnDespawned(ball);
            }
        }
        
        public class Args
        {
            public readonly float Size;
            public readonly int Price;
            public readonly Color Color;

            public Args(float size, int price, Color color)
            {
                Size = size;
                Price = price;
                Color = color;
            }
        }

    }
}