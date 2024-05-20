using System;
using TetrisPuzzle.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TetrisPuzzle.Managers
{
    public class GameController : MonoBehaviour
    {
        // Variables

        [SerializeField] private GameObject gameOverPanel;

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

        private bool isGameOver;

        // Events
        public event Action OnMoveShape;


        // Methods

        private void Start()
        {
            board = FindObjectOfType<Board>();
            shapeSpawner = FindObjectOfType<ShapeSpawner>();

            gameOverPanel.SetActive(false);
            activeShape = shapeSpawner.SpawnShape();
        }

        private void Update()
        {
            if (isGameOver) return;

            HandlePlayerInput();
        }

        private void HandlePlayerInput()
        {
            if (Input.GetButton("MoveRight") && Time.time > timeToNextKey)
            {
                timeToNextKey = Time.time + keyRepeatInterval;

                activeShape.MoveRight();
                OnMoveShape?.Invoke();

                if (!board.IsValidPosition(activeShape))
                {
                    activeShape.MoveLeft();
                }
            }
            else if (Input.GetButton("MoveLeft") && Time.time > timeToNextKey)
            {
                timeToNextKey = Time.time + keyRepeatInterval;

                activeShape.MoveLeft();
                OnMoveShape?.Invoke();

                if (!board.IsValidPosition(activeShape))
                {
                    activeShape.MoveRight();
                }
            }
            else if (Input.GetButtonDown("Rotate"))
            {
                activeShape.RotateRight();
                OnMoveShape?.Invoke();

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
                OnMoveShape?.Invoke();

                if (!board.IsValidPosition(activeShape))
                {
                    LandShape();
                }
            }
        }

        private void LandShape()
        {
            activeShape.MoveUp();

            if (!board.IsOverLimit(activeShape))
            {
                board.StoreShapeInGrid(activeShape);
                board.ClearAllCompletedRows();
                activeShape = shapeSpawner.SpawnShape();
            }
            else
            {
                isGameOver = true;
                gameOverPanel.SetActive(true);
            }
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(0);
        }
    }
}
