using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Helpers;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _musicAudioSource;

        [SerializeField]
        private AudioSource _effectsAudioSource;

        private Dictionary<SoundType, AudioClip> _audioClips = new Dictionary<SoundType, AudioClip>();

        private void Awake()
        {
            _audioClips.Add(SoundType.Music, Resources.Load<AudioClip>("Sounds/background music"));
            _audioClips.Add(SoundType.FoodIted, Resources.Load<AudioClip>("Sounds/food"));
            _audioClips.Add(SoundType.LifeLosed, Resources.Load<AudioClip>("Sounds/wall"));
            _musicAudioSource.clip = _audioClips[SoundType.Music];
            _musicAudioSource.loop = true;
            _effectsAudioSource.clip = _audioClips[SoundType.FoodIted];
            PlayMusic();
        }

        public void PlayMusic()
        {
            //TODO: add when settings will be realized
            //if (SettingsHelper.IsSoundEnabled)
            {
                _musicAudioSource.Play();
            }
        }

        public void PlayFoodItedSound()
        {
            //TODO: add when settings will be realized
            //if (SettingsHelper.IsSoundEnabled)
            {
                _effectsAudioSource.clip = _audioClips[SoundType.FoodIted];
                _effectsAudioSource.Play();
            }
        }

        public void PlayLifeLosedSound()
        {
            //TODO: add when settings will be realized
            //if (SettingsHelper.IsSoundEnabled)
            {
                _effectsAudioSource.clip = _audioClips[SoundType.LifeLosed];
                _effectsAudioSource.Play();
            }
        }
    }
}