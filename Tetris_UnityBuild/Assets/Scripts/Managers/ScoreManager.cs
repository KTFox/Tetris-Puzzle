using TetrisPuzzle.Core;
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

        // Properties

        public int Score => score;
        public int Lines => lines;
        public int Level => level;


        // Methods

        private void Start()
        {
            ResetScore();

            FindObjectOfType<Board>().OnClearRows += ScoreManager_OnClearRows;
        }

        private void ResetScore()
        {
            level = 1;
            lines = linesPerLevel * level;
        }

        private void ScoreManager_OnClearRows(int rows)
        {
            switch (rows)
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
