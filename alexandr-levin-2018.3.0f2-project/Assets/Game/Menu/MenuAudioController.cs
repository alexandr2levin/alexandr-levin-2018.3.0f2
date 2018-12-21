using Game.Base;
using UnityEngine;
using Zenject;

namespace Game.Menu
{
    public class MenuAudioController : MonoBehaviour
    {
        public AudioClip Clip;

        private AudioPlayer _audioPlayer;

        [Inject]
        public void InjectDependencies(AudioPlayer audioPlayer)
        {
            _audioPlayer = audioPlayer;
        }

        private void Start()
        {
            PlayMusic();
        }

        public void PlayMusic()
        {
            _audioPlayer.SetBackgroundMusic(Clip);
            _audioPlayer.FadeIn();
        }

        public void StopMusic()
        {
            _audioPlayer.FadeOut();
        }
        
    }
}