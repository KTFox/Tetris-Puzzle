using TetrisPuzzle.Core;
using UnityEngine;

namespace TetrisPuzzle.Managers
{
    public class GameController : MonoBehaviour
    {
        // Variables

        private Board board;
        private ShapeSpawner shapeSpawner;
        private Shape activeShape;
        private float shapeDroppingInterval = 0.5f;
        private float timeToDrop;

        // Used for left,right keys
        private float keyRepeatInterval = 0.1f;
        private float timeToNextKey;

        // Used for down key
        private float moveDownKeyRepeatInterval = 0.02f;
        private float timeToNextMoveDownKey;


        // Methods

        private void Start()
        {
            board = FindObjectOfType<Board>();
            shapeSpawner = FindObjectOfType<ShapeSpawner>();

            activeShape = shapeSpawner.SpawnShape();
        }

        private void Update()
        {
            HandlePlayerInput();
        }

        private void HandlePlayerInput()
        {
            if (Input.GetButton("MoveRight") && Time.time > timeToNextKey)
            {
                timeToNextKey = Time.time + keyRepeatInterval;

                activeShape.MoveRight();

                if (!board.IsValidPosition(activeShape))
                {
                    activeShape.MoveLeft();
                }
            }
            else if (Input.GetButton("MoveLeft") && Time.time > timeToNextKey)
            {
                timeToNextKey = Time.time + keyRepeatInterval;

                activeShape.MoveLeft();

                if (!board.IsValidPosition(activeShape))
                {
                    activeShape.MoveRight();
                }
            }
            else if (Input.GetButtonDown("Rotate"))
            {
                activeShape.RotateRight();

                if (!board.IsValidPosition(activeShape))
                {
                    activeShape.RotateLeft();
                }
            }
            else if ((Input.GetButton("MoveDown") && Time.time > timeToNextMoveDownKey) || Time.time > timeToDrop)
            {
                timeToDrop = Time.time + shapeDroppingInterval;
                timeToNextMoveDownKey = Time.time + moveDownKeyRepeatInterval;

                activeShape.MoveDown();

                if (!board.IsValidPosition(activeShape))
                {
                    LandShape();
                }
            }
        }

        private void LandShape()
        {
            activeShape.MoveUp();
            board.StoreShapeInGrid(activeShape);
            board.ClearAllCompletedRows();
            activeShape = shapeSpawner.SpawnShape();
        }
    }
}
