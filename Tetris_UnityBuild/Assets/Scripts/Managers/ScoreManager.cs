using UnityEngine;

namespace TetrisPuzzle
{
    public class ScoreManager : MonoBehaviour
    {
        // Variables

        [SerializeField] private int linesPerLevel = 5;

        private int score;
        private int lines;
        private int level = 1;


        // Methods

        private void Start()
        {
            ResetScore();
        }

        private void ResetScore()
        {
            level = 1;
            lines = linesPerLevel * level;
        }

        public void ScoreLines(int lines)
        {
            switch (lines)
            {
                case 1:
                    score += 40 * level;
                    break;
                case 2:
                    score += 100 * level;
                    break;
                case 3:
                    score += 300 * level;
                    break;
                case 4:
                    score += 1200 * level;
                    break;
            }
        }
    }
}
