using System.Collections;
using UnityEngine;

namespace Game.Base
{
    public class AudioPlayer : MonoBehaviour
    {
        
        private AudioSource _backgroundMusicAudioSource;
        private AudioSource _fxAudioSource;

        private void Awake()
        {
            _backgroundMusicAudioSource = gameObject.AddComponent<AudioSource>();
            _backgroundMusicAudioSource.loop = true;
            _fxAudioSource = gameObject.AddComponent<AudioSource>();
        }

        public void SetBackgroundMusic(AudioClip audioClip)
        {
            _backgroundMusicAudioSource.clip = audioClip;
            _backgroundMusicAudioSource.Play();
        }

        public void SetBackgroundMusicPitch(float pitch)
        {
            _backgroundMusicAudioSource.pitch = pitch;
        }

        public void PlayFx(AudioClip audioClip, float pitch)
        {
            _fxAudioSource.pitch = pitch;
            _fxAudioSource.PlayOneShot(audioClip);
        }

        public void FadeIn()
        {
            StartCoroutine(AnimateVolume(0f, 1f, 1f));
        }
        
        public void FadeOut()
        {
            StartCoroutine(AnimateVolume(1f, 0f, 1f));
        }

        private IEnumerator AnimateVolume(float from, float to, float timeSeconds)
        {
            var steps = 10;
            var tick = timeSeconds / steps;
            for (var i = 1; i <= steps; i++)
            {
                _backgroundMusicAudioSource.volume = Mathf.Lerp(from, to, (float) i / (float) steps);
                yield return new WaitForSeconds(tick);
            }
        }
        
    }
}