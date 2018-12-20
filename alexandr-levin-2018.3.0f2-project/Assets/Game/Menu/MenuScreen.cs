using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Screen = Game.Base.Screen;

namespace Game.Menu
{
    public class MenuScreen : Screen
    {

        public Button PlayButton;
        
        private Router _router;

        private Animator _animator;

        [Inject]
        private void InjectDependencies(Router router)
        {
            _router = router;
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            PlayButton.onClick.AddListener(ToGameplay);
        }

        private void ToGameplay()
        {
            _animator.SetTrigger("to_gameplay");
        }

        private void FinishToGameplay()
        {
            _router.ToGameplay();
        }
        
    }
}