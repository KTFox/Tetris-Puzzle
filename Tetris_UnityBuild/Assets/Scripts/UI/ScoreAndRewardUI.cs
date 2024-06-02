using TMPro;
using UnityEngine;

namespace TetrisPuzzle.UI
{
    public class ScoreAndRewardUI : MonoBehaviour
    {
        // Variables

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
            scoreText.text = $"Score: {scoreManager.Score}";
            rewardText.text = $"{scoreManager.Reward}";
        }
    }
}
