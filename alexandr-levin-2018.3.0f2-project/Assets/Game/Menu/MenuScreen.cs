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
        private MenuAudioController _menuAudioController;

        private Animator _animator;

        [Inject]
        private void InjectDependencies(Router router, MenuAudioController menuAudioController)
        {
            _router = router;
            _menuAudioController = menuAudioController;
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            PlayButton.onClick.AddListener(StartTransitionToGameplay);
        }

        private void StartTransitionToGameplay()
        {
            _menuAudioController.StopMusic();
            _animator.SetTrigger("to_gameplay");
        }

        // called from Animation by Animation Event
        private void FinishTransitionToGameplay()
        {
            _router.ToGameplay();
        }
        
    }
}