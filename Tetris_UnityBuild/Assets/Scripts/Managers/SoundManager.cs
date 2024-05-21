using System;
using TetrisPuzzle.Core;
using TetrisPuzzle.Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TetrisPuzzle
{
    public class SoundManager : MonoBehaviour
    {
        // Variables

        [SerializeField] private Sound[] musicSounds, sfxSounds;
        [SerializeField] private AudioSource musicSource, sfxSource;

        // Properties

        public bool IsMusicMuted => musicSource.mute;
        public bool IsSFXMuted => sfxSource.mute;

        // Structs

        [System.Serializable]
        public class Sound
        {
            public string Name;
            public AudioClip[] AudioClip;
        }


        // Methods

        private void Start()
        {
            SetMusicVolume(0.1f);
            SetSFXVolume(0.1f);
            PlayMusic("BackgroundMusic");

            FindObjectOfType<GameManager>().OnMoveShape += GameManager_OnMoveShape;
            FindObjectOfType<Board>().OnClearRows += Board_OnClearRows;
        }

        private void GameManager_OnMoveShape()
        {
            PlaySFX("MoveSFX");
        }

        private void Board_OnClearRows(int clearedRowAmount)
        {
            if (clearedRowAmount > 0)
            {
                PlaySFX("ClearRowSFX");

                if (clearedRowAmount > 1)
                {
                    PlaySFX("ClearMultipleRowsSFX");
                }
            }
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
