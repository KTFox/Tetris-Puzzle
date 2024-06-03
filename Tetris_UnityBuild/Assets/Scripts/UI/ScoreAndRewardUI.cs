using TMPro;
using UnityEngine;

namespace TetrisPuzzle.UI
{
    public class ScoreAndRewardUI : MonoBehaviour
    {
        // Variables

        [SerializeField] private TextMeshProUGUI highScoreText;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI rewardText;

        private ScoreManager scoreManager;


        // Methods

        private void Start()
        {
            scoreManager = FindObjectOfType<ScoreManager>();
        }

        private void Update()
        {
            highScoreText.text = $"High score: {scoreManager.HighScore.ToString()}";
            scoreText.text = $"Score: {scoreManager.Score}";
        }
    }
}
