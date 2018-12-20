using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Game.Gameplay.World.Ball
{
    [RequireComponent(typeof(Animator))]
    public class BallController : MonoBehaviour, IPointerClickHandler
    {

        public event Action<BallController> OnExploded;

        public Bounds Bounds
        {
            get
            {
                var bounds = _renderer.bounds;
                bounds.Encapsulate(TailSpriteRenderer.bounds);
                return bounds;
            }
        }

        public float BaseSpeed;
        public SpriteRenderer TailSpriteRenderer;

        private float _size;
        private int _price;
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
                var sizeFactor = _size * 0.8f;
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
            _size = args.Size;
            _price = args.Price;
            
            transform.localScale = new Vector3(_size, _size, _size);
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
            _session.ChangeScore(_price);
        }

        // called from Animation by Animation Event
        private void FinishExplode()
        {
            OnExploded?.Invoke(this);
        }
        
        public class Pool : MonoMemoryPool<Args, BallController>
        {
            protected override void Reinitialize(Args args, BallController ball)
            {
                ball.OnSpawned(args);
            }

            protected override void OnDespawned(BallController ball)
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