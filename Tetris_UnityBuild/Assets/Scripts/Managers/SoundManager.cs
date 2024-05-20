using System;
using TetrisPuzzle.Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TetrisPuzzle
{
    public class SoundManager : MonoBehaviour
    {
        // Variables

        public static SoundManager Instance;

        [SerializeField] private Sound[] musicSounds, sfxSounds;
        [SerializeField] private AudioSource musicSource, sfxSource;

        // Structs

        [System.Serializable]
        public class Sound
        {
            public string Name;
            public AudioClip[] AudioClip;
        }


        // Methods

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        private void Start()
        {
            SetMusicVolume(0.5f);
            SetSFXVolume(0.5f);
            PlayMusic("BackgroundMusic");

            FindObjectOfType<GameController>().OnMoveShape += SoundManager_OnMoveShape;
        }

        private void SoundManager_OnMoveShape()
        {
            PlaySFX("MoveSFX");
        }

        public void PlayMusic(string name)
        {
            Sound sound = Array.Find(musicSounds, x => x.Name == name);
            if (sound != null)
            {
                musicSource.clip = sound.AudioClip[0];
                musicSource.Play();
            }
        }

        public void PlaySFX(string name)
        {
            Sound sound = Array.Find(sfxSounds, x => x.Name == name);
            if (sound != null)
            {
                sfxSource.PlayOneShot(sound.AudioClip[Random.Range(0, sound.AudioClip.Length)]);
            }
        }

        public void ToggleMusic()
        {
            musicSource.mute = !musicSource.mute;
        }

        public void ToggleSFX()
        {
            sfxSource.mute = !sfxSource.mute;
        }

        public void SetMusicVolume(float volume)
        {
            musicSource.volume = volume;
        }

        public void SetSFXVolume(float volume)
        {
            sfxSource.volume = volume;
        }
    }
}
