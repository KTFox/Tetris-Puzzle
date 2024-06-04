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

        private const string IS_MUSIC_MUTED = "IsMusicMuted";
        private const string IS_SFX_MUTED = "IsSFXMuted";

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

        private void Awake()
        {
            if (PlayerPrefs.GetInt(IS_MUSIC_MUTED) == 0)
            {
                musicSource.mute = true;
            }
            else
            {
                musicSource.mute = false;
            }

            if (PlayerPrefs.GetInt(IS_SFX_MUTED) == 0)
            {
                sfxSource.mute = true;
            }
            else
            {
                sfxSource.mute = false;
            }
        }

        private void Start()
        {
            PlayMusic("BackgroundMusic", 0.1f);

            FindObjectOfType<GameManager>().OnMoveShape += GameManager_OnMoveShape;
            FindObjectOfType<Board>().OnClearRows += Board_OnClearRows;
        }

        private void GameManager_OnMoveShape()
        {
            PlaySFX("MoveSFX", 0.1f);
        }

        private void Board_OnClearRows(int clearedRowAmount)
        {
            if (clearedRowAmount > 0)
            {
                PlaySFX("ClearRowSFX", 0.1f);
            }
        }

        public void PlayMusic(string name, float volume)
        {
            Sound sound = Array.Find(musicSounds, x => x.Name == name);
            if (sound != null)
            {
                musicSource.clip = sound.AudioClip[0];
                musicSource.volume = volume;
                musicSource.Play();
            }
            else
            {
                Debug.LogWarning($"{name} is missing!!!");
            }
        }

        public void PlaySFX(string name, float volume)
        {
            Sound sound = Array.Find(sfxSounds, x => x.Name == name);
            if (sound != null)
            {
                sfxSource.PlayOneShot(sound.AudioClip[Random.Range(0, sound.AudioClip.Length)], volume);
            }
            else
            {
                Debug.LogWarning($"{name} is missing!!!");
            }
        }

        public void ToggleMusic()
        {
            musicSource.mute = !musicSource.mute;

            if (musicSource.mute)
            {
                PlayerPrefs.SetInt(IS_MUSIC_MUTED, 0);
            }
            else
            {
                PlayerPrefs.SetInt(IS_MUSIC_MUTED, 1);
            }
        }

        public void ToggleSFX()
        {
            sfxSource.mute = !sfxSource.mute;

            if (sfxSource.mute)
            {
                PlayerPrefs.SetInt(IS_SFX_MUTED, 0);
            }
            else
            {
                PlayerPrefs.SetInt(IS_SFX_MUTED, 1);
            }
        }
    }
}
