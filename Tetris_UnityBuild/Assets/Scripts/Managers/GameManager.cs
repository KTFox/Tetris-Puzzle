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
        private float accelerationFactor = 500f;
        private float timeToDrop;

        // Used for left,right keys
        private const string MIN_TIME_TO_DRAG = "minTimeToDrag";
        private float minTimeToDrag = 0.5f;
        private float minTimeToSwipe = 0.3f;
        private float timeToNextDrag;
        private float timeToNextSwipe;

        // Used for Touch Input
        private SwipeDirection dragDirection;
        private SwipeDirection swipeDirection;

        private bool didTap;

        private Shape activeShape;
        private bool isGamePaused;
        private bool isGameOver;

        // Properties

        public float MinTimeToDrag
        {
            set
            {
                minTimeToDrag = value;
                PlayerPrefs.SetFloat(MIN_TIME_TO_DRAG, minTimeToDrag);
                PlayerPrefs.Save();
            }

            get => minTimeToDrag;
        }

        // Events

        public event Action OnMoveShape;
        public event Action OnHoldShape;
        public event Action OnFailHoldShape;

        // enums

        private enum SwipeDirection { none, right, left, up, down }


        // Methods

        private void OnEnable()
        {
            TouchManager.OnDrag += TouchManager_OnDrag;
            TouchManager.OnSwipe += TouchManager_OnSwipe;
            TouchManager.OnTap += TouchManager_OnTap;
        }

        private void OnDisable()
        {
            TouchManager.OnDrag -= TouchManager_OnDrag;
            TouchManager.OnSwipe -= TouchManager_OnSwipe;
            TouchManager.OnTap -= TouchManager_OnTap;
        }

        private void Awake()
        {
            MinTimeToDrag = PlayerPrefs.GetFloat(MIN_TIME_TO_DRAG);
        }

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
            if (isGamePaused || isGameOver) return;

            HandlePlayerInput();
        }

        private void LateUpdate()
        {
            ghostDrawer.DrawGhost(activeShape);
        }

        private void TouchManager_OnDrag(Vector2 touchMovement)
        {
            dragDirection = GetSwipeDirection(touchMovement);
        }

        private void TouchManager_OnSwipe(Vector2 touchMovement)
        {
            swipeDirection = GetSwipeDirection(touchMovement);
        }

        private SwipeDirection GetSwipeDirection(Vector2 swipeMovement)
        {
            SwipeDirection swipeDir = SwipeDirection.none;

            if (Mathf.Abs(swipeMovement.x) >= Mathf.Abs(swipeMovement.y))
            {
                swipeDir = swipeMovement.x >= 0 ? SwipeDirection.right : SwipeDirection.left;
            }
            else
            {
                swipeDir = swipeMovement.y >= 0 ? SwipeDirection.up : SwipeDirection.down;
            }

            return swipeDir;
        }

        private void TouchManager_OnTap()
        {
            didTap = true;
        }

        private void HandlePlayerInput()
        {
            if ((dragDirection == SwipeDirection.right && Time.time > timeToNextDrag) || (swipeDirection == SwipeDirection.right && Time.time > timeToNextSwipe))
            {
                MoveShapeToTheRight();
            }
            else if ((dragDirection == SwipeDirection.left && Time.time > timeToNextDrag) || (swipeDirection == SwipeDirection.left && Time.time > timeToNextSwipe))
            {
                MoveShapeToTheLeft();
            }
            else if (didTap == true)
            {
                RotateShape();
            }
            else if (dragDirection == SwipeDirection.down || Time.time > timeToDrop)
            {
                MoveShapeDown();
            }

            dragDirection = SwipeDirection.none;
            swipeDirection = SwipeDirection.none;
            didTap = false;
        }

        private void MoveShapeToTheRight()
        {
            timeToNextDrag = Time.time + minTimeToDrag;
            timeToNextSwipe = Time.time + minTimeToSwipe;

            activeShape.MoveRight();
            OnMoveShape?.Invoke();

            if (board.HasReachedRightLimit(activeShape) || board.IsOccupied(activeShape))
            {
                activeShape.MoveLeft();
            }
        }

        private void MoveShapeToTheLeft()
        {
            timeToNextDrag = Time.time + minTimeToDrag;

            activeShape.MoveLeft();
            OnMoveShape?.Invoke();

            if (board.HasReachedLeftLimit(activeShape) || board.IsOccupied(activeShape))
            {
                activeShape.MoveRight();
            }
        }

        private void RotateShape()
        {
            timeToNextSwipe = Time.time + minTimeToSwipe;
            didTap = false;

            Vector3 originalPos = activeShape.transform.position;
            Quaternion originalRotation = activeShape.transform.rotation;

            activeShape.RotateRight();
            OnMoveShape?.Invoke();

            while (board.HasReachedRightLimit(activeShape))
            {
                activeShape.MoveLeft();
            }
            while (board.HasReachedLeftLimit(activeShape))
            {
                activeShape.MoveRight();
            }
            if (board.IsOccupied(activeShape))
            {
                activeShape.transform.position = originalPos;
                activeShape.transform.rotation = originalRotation;
            }
        }

        private void MoveShapeDown()
        {
            timeToDrop = Time.time + GetDroppingInterval();
            timeToNextDrag = Time.time + minTimeToDrag;

            activeShape.MoveDown();
            OnMoveShape?.Invoke();

            if (board.HasReachedBoardFloor(activeShape) || board.IsOccupied(activeShape))
            {
                LandShape();
            }
        }

        private float GetDroppingInterval()
        {
            return defaultDroppingInterval / (1 + scoreManager.Score / accelerationFactor);
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
            isGamePaused = !isGamePaused;
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(1);
        }
    }
}
