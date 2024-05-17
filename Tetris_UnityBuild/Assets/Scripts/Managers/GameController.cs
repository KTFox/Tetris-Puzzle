using TetrisPuzzle.Core;
using UnityEngine;

namespace TetrisPuzzle.Managers
{
    public class GameController : MonoBehaviour
    {
        // Variables

        private Board board;
        private ShapeSpawner shapeSpawner;


        // Methods

        private void Start()
        {
            board = FindObjectOfType<Board>();
            shapeSpawner = FindObjectOfType<ShapeSpawner>();
        }
    }
}
