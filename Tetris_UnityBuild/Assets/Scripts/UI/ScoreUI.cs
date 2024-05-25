using TMPro;
using UnityEngine;

namespace TetrisPuzzle.UI
{
    public class ScoreUI : MonoBehaviour
    {
        // Variables

        [SerializeField] private TextMeshProUGUI scoreText;

        private ScoreManager scoreManager;


        // Methods

        private void Start()
        {
            scoreManager = FindObjectOfType<ScoreManager>();
        }

        private void Update()
        {
            scoreText.text = GetPadZero(scoreManager.Score, 7);
        }

        private string GetPadZero(int number, int padDigits)
        {
            string str = number.ToString();
            while (str.Length < padDigits)
            {
                str = "0" + str;
            }

            return str;
        }
    }
}
