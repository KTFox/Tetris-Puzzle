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
            SetMusicVolume(0.5f);
            SetSFXVolume(1f);
            PlayMusic("BackgroundMusic");

            FindObjectOfType<GameManager>().OnMoveShape += GameManager_OnMoveShape;
            FindObjectOfType<GameManager>().OnHoldShape += GameManager_OnHoldShape;
            FindObjectOfType<GameManager>().OnFailHoldShape += GameManager_OnFailHoldShape;
            FindObjectOfType<Board>().OnClearRows += Board_OnClearRows;
            FindObjectOfType<ScoreManager>().OnLevelUp += SoundManager_OnLevelUp;
        }

        private void GameManager_OnMoveShape()
        {
            PlaySFX("MoveSFX");
        }

        private void GameManager_OnHoldShape()
        {
            PlaySFX("HoldSFX");
        }

        private void GameManager_OnFailHoldShape()
        {
            PlaySFX("FailHoldSFX");
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

        private void SoundManager_OnLevelUp()
        {
            PlaySFX("LevelUpSFX");
        }

        public void PlayMusic(string name)
        {
            Sound sound = Array.Find(musicSounds, x => x.Name == name);
            if (sound != null)
            {
                musicSource.clip = sound.AudioClip[0];
                musicSource.Play();
            }
            else
            {
                Debug.LogWarning($"{name} is missing!!!");
            }
        }

        public void PlaySFX(string name)
        {
            Sound sound = Array.Find(sfxSounds, x => x.Name == name);
            if (sound != null)
            {
                sfxSource.PlayOneShot(sound.AudioClip[Random.Range(0, sound.AudioClip.Length)]);
            }
            else
            {
                Debug.LogWarning($"{name} is missing!!!");
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
