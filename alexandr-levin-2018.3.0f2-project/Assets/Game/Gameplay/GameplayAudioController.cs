using Game.Base;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class GameplayAudioController : MonoBehaviour
    {
        public AudioClip Clip;
        public float MaxPitch;

        private AudioPlayer _audioPlayer;
        private GameSession _gameSession;

        [Inject]
        public void InjectDependencies(AudioPlayer audioPlayer, GameSession gameSession)
        {
            _audioPlayer = audioPlayer;
            _gameSession = gameSession;
            _gameSession.OnTimerProgressChanged += UpdatePitchByTimerProgress;
            _gameSession.OnBegin += PlayMusic;
            _gameSession.OnFinish += StopMusic;
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

        private void UpdatePitchByTimerProgress(float progress)
        {
            var pitch = Mathf.Lerp(1f, MaxPitch, progress);
            _audioPlayer.SetBackgroundMusicPitch(pitch);
        }
        
    }
}