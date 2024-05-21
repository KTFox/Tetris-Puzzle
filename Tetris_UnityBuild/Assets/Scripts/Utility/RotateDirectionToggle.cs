using TetrisPuzzle.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace TetrisPuzzle.Utilities
{
    public class RotateDirectionToggle : MonoBehaviour
    {
        // Variables

        [SerializeField] private Sprite rotateRightIcon;
        [SerializeField] private Sprite rotateLeftIcon;

        private Image image;
        private GameController gameController;


        // Methods

        private void Awake()
        {
            image = GetComponent<Image>();
        }

        private void Start()
        {
            gameController = FindObjectOfType<GameController>();

            image.sprite = gameController.IsRotateRight ? rotateRightIcon : rotateLeftIcon;
        }

        public void ChangeDirection()
        {
            image.sprite = (image.sprite == rotateRightIcon) ? rotateLeftIcon : rotateRightIcon;
            gameController.ChangeRotateDirection();
        }
    }
}
