using System;
using TetrisPuzzle.Core;
using UnityEngine;

namespace TetrisPuzzle
{
    public class ScoreManager : MonoBehaviour
    {
        // Variables

        private const string HIGH_SCORE = "HighScore";

        private int score;

        // Properties

        public int HighScore => PlayerPrefs.GetInt(HIGH_SCORE);
        public int Score => score;


        // Methods

        private void Start()
        {
            FindObjectOfType<Board>().OnClearRows += ScoreManager_OnClearLines;
        }

        private void ScoreManager_OnClearLines(int lines)
        {
            switch (lines)
            {
                case 1:
                    score += 5;
                    break;
                case 2:
                    score += 10;
                    break;
                case 3:
                    score += 20;
                    break;
                case 4:
                    score += 40;
                    break;
            }

            if (score > PlayerPrefs.GetInt(HIGH_SCORE, 0))
            {
                PlayerPrefs.SetInt(HIGH_SCORE, score);
            }
        }
    }
}
