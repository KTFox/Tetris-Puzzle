using TetrisPuzzle.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TetrisPuzzle
{
    public class PausePanel : MonoBehaviour
    {
        // Methods

        public void Resume()
        {
            FindObjectOfType<GameManager>().TogglePauseGame();
        }

        public void Restart()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        }
    }
}
