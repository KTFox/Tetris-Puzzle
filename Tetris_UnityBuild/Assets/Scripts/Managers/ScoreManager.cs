using System;
using TetrisPuzzle.Core;
using UnityEngine;

namespace TetrisPuzzle
{
    public class ScoreManager : MonoBehaviour
    {
        // Variables

        private int linesPerLevel = 2;
        private int score;
        private int lines;
        private int level = 1;

        // Properties

        public int Score => score;
        public int Lines => lines;
        public int Level => level;

        // Events

        public event Action OnLevelUp;


        // Methods

        private void Start()
        {
            ResetScore();

            FindObjectOfType<Board>().OnClearRows += ScoreManager_OnClearLines;
        }

        private void ResetScore()
        {
            level = 1;
            lines = linesPerLevel * level;
        }

        private void ScoreManager_OnClearLines(int lines)
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

            this.lines -= lines;
            if (this.lines <= 0)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            level++;
            lines = linesPerLevel * level;

            OnLevelUp?.Invoke();
        }
    }
}
