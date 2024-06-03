using TetrisPuzzle.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace TetrisPuzzle.UI
{
    public class SettingPanel : MonoBehaviour
    {
        // Variables

        [SerializeField] private Slider dragSensitiveSlider;

        private GameManager gameManager;


        // Methods

        private void Start()
        {
            gameManager = FindObjectOfType<GameManager>();

            dragSensitiveSlider.value = gameManager.MinTimeToDrag;
        }

        public void UpdateSlider()
        {
            gameManager.MinTimeToDrag = 1 - dragSensitiveSlider.value;
        }
    }
}
