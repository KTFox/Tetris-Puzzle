using TMPro;
using UnityEngine;

namespace TetrisPuzzle.UI
{
    public class LevelUI : MonoBehaviour
    {
        // Variables

        [SerializeField] private TextMeshProUGUI levelText;

        private ScoreManager scoreManager;


        // Methods

        private void Start()
        {
            scoreManager = FindObjectOfType<ScoreManager>();
        }

        private void Update()
        {
            levelText.text = scoreManager.Level.ToString();
        }
    }
}
