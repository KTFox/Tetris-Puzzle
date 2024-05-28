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
        private GhostDrawer ghostDrawer;
        private ShapeHolder shapeHolder;

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
        private bool isGameOver;

        // Events

        public event Action OnMoveShape;
        public event Action OnHoldShape;
        public event Action OnFailHoldShape;


        // Methods

        private void Start()
        {
            scoreManager = FindObjectOfType<ScoreManager>();
            board = FindObjectOfType<Board>();
            shapeSpawner = FindObjectOfType<ShapeSpawner>();
            ghostDrawer = FindObjectOfType<GhostDrawer>();
            shapeHolder = FindObjectOfType<ShapeHolder>();

            gameOverPanel.SetActive(false);
            pausePanel.SetActive(false);

            activeShape = shapeSpawner.SpawnShape();
        }

        private void Update()
        {
            if (isGameOver) return;

            HandlePlayerInput();
        }

        private void LateUpdate()
        {
            ghostDrawer.DrawGhost(activeShape);
        }

        private void HandlePlayerInput()
        {
            if (Input.GetButton("MoveRight") && Time.time > timeToNextKey)
            {
                timeToNextKey = Time.time + keyRepeatInterval;

                activeShape.MoveRight();
                OnMoveShape?.Invoke();

                if (!board.IsValidShapePosition(activeShape))
                {
                    activeShape.MoveLeft();
                }
            }
            else if (Input.GetButton("MoveLeft") && Time.time > timeToNextKey)
            {
                timeToNextKey = Time.time + keyRepeatInterval;

                activeShape.MoveLeft();
                OnMoveShape?.Invoke();

                if (!board.IsValidShapePosition(activeShape))
                {
                    activeShape.MoveRight();
                }
            }
            else if (Input.GetButtonDown("Rotate"))
            {
                activeShape.RotateRight();

                OnMoveShape?.Invoke();

                if (!board.IsValidShapePosition(activeShape))
                {
                    activeShape.RotateLeft();
                }
            }
            else if ((Input.GetButton("MoveDown") && Time.time > timeToNextMoveDownKey) || Time.time > timeToDrop)
            {
                timeToDrop = Time.time + GetDroppingInterval();
                timeToNextMoveDownKey = Time.time + moveDownKeyRepeatInterval;

                activeShape.MoveDown();
                OnMoveShape?.Invoke();

                if (!board.IsValidShapePosition(activeShape))
                {
                    LandShape();
                }
            }
            else if (Input.GetButtonDown("Hold"))
            {
                HandleHoldingShape();
            }
        }

        private float GetDroppingInterval()
        {
            return defaultDroppingInterval * Mathf.Pow(1 - accelerationFactor, scoreManager.Level - 1);
        }

        private void LandShape()
        {
            activeShape.MoveUp();

            if (!board.HasReachedBoardRoof(activeShape))
            {
                activeShape.PlayLandingFX();
                board.StoreShapeInGrid(activeShape);
                board.StartCoroutine(nameof(board.ClearAllCompletedRows));
                ghostDrawer.ResetGhostShape();
                activeShape = shapeSpawner.SpawnShape();
                shapeHolder.SetCanSwitch(true);
            }
            else
            {
                isGameOver = true;
                gameOverPanel.SetActive(true);
            }
        }

        public void HandleHoldingShape()
        {
            if (shapeHolder.IsEmptyHolder)
            {
                shapeHolder.HoldShape(activeShape);
                activeShape = shapeSpawner.SpawnShape();
                ghostDrawer.ResetGhostShape();

                OnHoldShape?.Invoke();
            }
            else if (!shapeHolder.IsEmptyHolder && shapeHolder.CanSwitch)
            {
                Shape shapeToHold = activeShape;

                activeShape = shapeHolder.ReleaseShape();
                activeShape.transform.position = shapeSpawner.SpawnPosition;

                shapeHolder.HoldShape(shapeToHold);
                ghostDrawer.ResetGhostShape();

                OnHoldShape?.Invoke();
            }
            else
            {
                OnFailHoldShape?.Invoke();
            }
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
