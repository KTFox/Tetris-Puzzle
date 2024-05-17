using TetrisPuzzle.Core;
using UnityEngine;

namespace TetrisPuzzle.Managers
{
    public class GameController : MonoBehaviour
    {
        // Variables

        [SerializeField] private float dropInterval;

        private Board board;
        private ShapeSpawner shapeSpawner;
        private Shape activeShape;
        private float timeToDrop;


        // Methods

        private void Start()
        {
            board = FindObjectOfType<Board>();
            shapeSpawner = FindObjectOfType<ShapeSpawner>();

            activeShape = shapeSpawner.SpawnShape();
        }

        private void Update()
        {
            if (Time.time > timeToDrop)
            {
                timeToDrop = Time.time + dropInterval;

                if (activeShape != null)
                {
                    activeShape.MoveDown();

                    if (!board.IsValidPosition(activeShape))
                    {
                        activeShape.MoveUp();
                        board.StoreShapeInGrid(activeShape);

                        activeShape = shapeSpawner.SpawnShape();
                    }
                }
            }
        }
    }
}
