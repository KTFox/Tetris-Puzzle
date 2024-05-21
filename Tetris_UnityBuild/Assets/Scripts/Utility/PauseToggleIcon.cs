using UnityEngine;
using UnityEngine.UI;

namespace TetrisPuzzle.Utilities
{
    public class PauseToggleIcon : MonoBehaviour
    {
        // Variables

        [SerializeField] private Sprite pauseIcon;
        [SerializeField] private Sprite resumeIcon;

        private Image image;


        // Methods

        private void Awake()
        {
            image = GetComponent<Image>();
        }

        private void Start()
        {
            image.sprite = pauseIcon;
        }

        public void ChangeIcon()
        {
            image.sprite = image.sprite == pauseIcon ? resumeIcon : pauseIcon;
        }
    }
}
