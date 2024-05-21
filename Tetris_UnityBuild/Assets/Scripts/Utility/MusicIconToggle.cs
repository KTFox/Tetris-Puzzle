using UnityEngine;
using UnityEngine.UI;

namespace TetrisPuzzle.Utilities
{
    public class MusicIconToggle : MonoBehaviour
    {
        // Variables

        [SerializeField] private Sprite activeIcon;
        [SerializeField] private Sprite inactiveIcon;

        private Image image;
        private SoundManager soundManager;


        // Methods

        private void Awake()
        {
            image = GetComponent<Image>();
            soundManager = FindObjectOfType<SoundManager>();
        }

        private void Start()
        {
            image.sprite = soundManager.IsMusicMuted ? inactiveIcon : activeIcon;
        }

        #region Unity Events
        public void ToggleMusic()
        {
            image.sprite = (image.sprite == activeIcon) ? inactiveIcon : activeIcon;
            soundManager.ToggleMusic();
        }
        #endregion
    }
}
