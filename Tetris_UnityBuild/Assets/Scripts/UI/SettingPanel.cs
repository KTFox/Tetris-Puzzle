using TetrisPuzzle.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace TetrisPuzzle.UI
{
    public class SettingPanel : MonoBehaviour
    {
        // Variables

        [SerializeField] private Slider dragSensitiveSlider;
        [SerializeField] private ToggleButton toggleMusicButton;
        [SerializeField] private ToggleButton toggleSoundButton;

        private GameManager gameManager;
        private SoundManager soundManager;

        // Structs

        [System.Serializable]
        private struct ToggleButton
        {
            public Button Button;
            public Image ButtonImage;
            public Sprite ActiveSprite;
            public Sprite InactiveSprite;

            public void SetState(bool isActive)
            {
                if (isActive)
                {
                    ButtonImage.sprite = ActiveSprite;
                }
                else
                {
                    ButtonImage.sprite = InactiveSprite;
                }
            }
        }


        // Methods

        private void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            soundManager = FindObjectOfType<SoundManager>();

            toggleMusicButton.Button.onClick.AddListener(() =>
            {
                soundManager.ToggleMusic();
                toggleMusicButton.SetState(!soundManager.IsMusicMuted);
            });

            toggleSoundButton.Button.onClick.AddListener(() =>
            {
                soundManager.ToggleSFX();
                toggleSoundButton.SetState(!soundManager.IsSFXMuted);
            });

            dragSensitiveSlider.value = 1 - gameManager.MinTimeToDrag;
            toggleMusicButton.SetState(!soundManager.IsMusicMuted);
            toggleSoundButton.SetState(!soundManager.IsSFXMuted);
        }

        public void UpdateSlider()
        {
            gameManager.MinTimeToDrag = 1 - dragSensitiveSlider.value;
        }
    }
}
