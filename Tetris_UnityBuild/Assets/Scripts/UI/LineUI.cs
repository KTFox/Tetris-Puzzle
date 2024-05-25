using TMPro;
using UnityEngine;

namespace TetrisPuzzle.UI
{
    public class LineUI : MonoBehaviour
    {
        // Variables

        [SerializeField] private TextMeshProUGUI lineText;

        private ScoreManager scoreManager;


        // Methods

        private void Start()
        {
            scoreManager = FindObjectOfType<ScoreManager>();
        }

        private void Update()
        {
            lineText.text = scoreManager.Lines.ToString();
        }
    }
}
