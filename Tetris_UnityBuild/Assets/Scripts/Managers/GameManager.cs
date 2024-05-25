using System;
using TetrisPuzzle.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TetrisPuzzle.Managers
{
    public class GameManager : MonoBehaviour
    {
        // Variables

        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private GameObject pausePanel;

        // Caching 
        private ScoreManager scoreManager;
        private Board board;
        private ShapeSpawner shapeSpawner;

        // Drop speed balancing
        private float defaultDroppingInterval = 0.5f;
        private float accelerationFactor = 0.1f;
        private float timeToDrop;

        // Used for left,right keys
        private float keyRepeatInterval = 0.1f;
        private float timeToNextKey;

        // Used for down key
        private float moveDownKeyRepeatInterval = 0.05f;
        private float timeToNextMoveDownKey;

        private Shape activeShape;
        private bool isRotateRight = true;
        private bool isGameOver;

        // Properties

        public bool IsRotateRight => isRotateRight;

        // Events

        public event Action OnMoveShape;


        // Methods

        private void Start()
        {
            scoreManager = FindObjectOfType<ScoreManager>();
            board = FindObjectOfType<Board>();
            shapeSpawner = FindObjectOfType<ShapeSpawner>();

            gameOverPanel.SetActive(false);
            pausePanel.SetActive(false);
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
                if (isRotateRight)
                {
                    activeShape.RotateRight();
                }
                else
                {
                    activeShape.RotateLeft();
                }

                OnMoveShape?.Invoke();

                if (!board.IsValidPosition(activeShape))
                {
                    if (isRotateRight)
                    {
                        activeShape.RotateLeft();
                    }
                    else
                    {
                        activeShape.RotateRight();
                    }
                }
            }
            else if ((Input.GetButton("MoveDown") && Time.time > timeToNextMoveDownKey) || Time.time > timeToDrop)
            {
                timeToDrop = Time.time + GetDroppingInterval();
                timeToNextMoveDownKey = Time.time + moveDownKeyRepeatInterval;

                activeShape.MoveDown();
                OnMoveShape?.Invoke();

                if (!board.IsValidPosition(activeShape))
                {
                    LandShape();
                }
            }
        }

        private float GetDroppingInterval()
        {
            return defaultDroppingInterval * Mathf.Pow(1 - accelerationFactor, scoreManager.Level - 1);
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

        public void ChangeRotateDirection()
        {
            isRotateRight = !isRotateRight;
        }

        public void TogglePauseGame()
        {
            Time.timeScale = Time.timeScale == 1 ? 0 : 1;
            pausePanel.SetActive(!pausePanel.gameObject.activeSelf);
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(0);
        }
    }
}
