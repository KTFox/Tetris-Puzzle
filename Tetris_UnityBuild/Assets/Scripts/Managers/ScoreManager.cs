using System;
using TetrisPuzzle.Core;
using UnityEngine;

namespace TetrisPuzzle
{
    public class ScoreManager : MonoBehaviour
    {
        // Variables

        private int score;
        private int reward;

        // Properties

        public int Score => score;
        public int Reward => reward;


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
                    reward += 1;
                    break;
                case 2:
                    score += 10;
                    reward += 2;
                    break;
                case 3:
                    score += 20;
                    reward += 3;
                    break;
                case 4:
                    score += 40;
                    reward += 4;
                    break;
            }
        }
    }
}
