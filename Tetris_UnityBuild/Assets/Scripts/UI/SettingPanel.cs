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

        // Structs

        [System.Serializable]
        private struct ToggleButton
        {
            public Button Button;
            public Image ButtonImage;
            public Sprite ActiveSprite;
            public Sprite InactiveSprite;
        }


        // Methods

        private void Start()
        {
            gameManager = FindObjectOfType<GameManager>();

            toggleMusicButton.Button.onClick.AddListener(() =>
            {
                FindObjectOfType<SoundManager>().ToggleMusic();

                Image buttonImage = toggleMusicButton.ButtonImage;
                buttonImage.sprite = buttonImage.sprite == toggleMusicButton.ActiveSprite ? toggleMusicButton.InactiveSprite : toggleMusicButton.ActiveSprite;
            });

            toggleSoundButton.Button.onClick.AddListener(() =>
            {
                FindObjectOfType<SoundManager>().ToggleSFX();

                Image buttonImage = toggleSoundButton.ButtonImage;
                buttonImage.sprite = buttonImage.sprite == toggleSoundButton.ActiveSprite ? toggleSoundButton.InactiveSprite : toggleSoundButton.ActiveSprite;
            });

            dragSensitiveSlider.value = 1 - gameManager.MinTimeToDrag;
        }

        public void UpdateSlider()
        {
            gameManager.MinTimeToDrag = 1 - dragSensitiveSlider.value;
        }
    }
}
