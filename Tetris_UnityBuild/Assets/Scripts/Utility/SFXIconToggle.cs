using UnityEngine;
using UnityEngine.UI;

namespace TetrisPuzzle.Utilities
{
    public class SFXIconToggle : MonoBehaviour
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
            image.sprite = soundManager.IsSFXMuted ? inactiveIcon : activeIcon;
        }

        #region Unity Events
        public void ToggleSFX()
        {
            image.sprite = (image.sprite == activeIcon) ? inactiveIcon : activeIcon;
            soundManager.ToggleSFX();
        }
        #endregion
    }
}
