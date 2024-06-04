using UnityEngine;
using UnityEngine.SceneManagement;

namespace TetrisPuzzle.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        // Methods

        public void PlayGame()
        {
            SceneManager.LoadScene(1);
        }
    }
}
