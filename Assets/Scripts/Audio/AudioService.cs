using UnityEngine;
using UnityEngine.Assertions;

namespace Playground.Audio
{
    public class AudioService
    {
        #region Variables

        private const string ConfigPath = "Configs/Audio/AudioServiceConfig";
        private readonly Transform _rootTransform;
        private AudioServiceConfig _config;
        private Transform _serviceRootTransform;

        private AudioSource _soundAudioSource;

        #endregion

        #region Setup/Teardown

        public AudioService(Transform rootTransform)
        {
            _rootTransform = rootTransform;
        }

        #endregion

        #region Public methods

        public void Init()
        {
            LoadConfig();
            CreateRootObject();
            CreateSoundSource();
        }

        public void PlaySound(SoundType type)
        {
            AudioClip clip = _config.GetSound(type);
            PlaySoundClip(clip);
        }

        #endregion

        #region Private methods

        private void CreateRootObject()
        {
            _serviceRootTransform = new GameObject($"[{nameof(AudioService)}]").transform;
            _serviceRootTransform.SetParent(_rootTransform);
        }

        private void CreateSoundSource()
        {
            GameObject go = new("SoundsSource");
            _soundAudioSource = go.AddComponent<AudioSource>();
            go.transform.SetParent(_serviceRootTransform);
        }

        private void LoadConfig()
        {
            Debug.Log("LoadConfig");
            _config = Resources.Load<AudioServiceConfig>(ConfigPath);
            Assert.IsNotNull(_config);
        }

        private void PlaySoundClip(AudioClip clip)
        {
            if (clip == null)
            {
                return;
            }

            _soundAudioSource.PlayOneShot(clip);
        }

        #endregion
    }
}