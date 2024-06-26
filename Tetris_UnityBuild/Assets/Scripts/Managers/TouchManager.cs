using System;
using TMPro;
using UnityEngine;

namespace TetrisPuzzle.Managers
{
    public class TouchManager : MonoBehaviour
    {
        // Variables

        [SerializeField] private BoxCollider2D touchArea;
        [SerializeField] private float tapTimeWindow = 0.1f;

        private Vector2 touchMovement;
        private float tapTimeMax;
        private int minDragDistance = 100;
        private int minSwipeDistance = 200;

        // Events

        public static event Action<Vector2> OnDrag;
        public static event Action<Vector2> OnSwipe;
        public static event Action OnTap;


        // Methods

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.touches[0];

                if (touchArea.OverlapPoint(Camera.main.ScreenToWorldPoint(touch.position)))
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        touchMovement = Vector2.zero;
                        tapTimeMax = Time.time + tapTimeWindow;
                    }
                    else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                    {
                        touchMovement += touch.deltaPosition;

                        if (touchMovement.magnitude >= minDragDistance)
                        {
                            OnDrag?.Invoke(touchMovement);
                        }
                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
                        if (touchMovement.magnitude >= minSwipeDistance)
                        {
                            OnSwipe?.Invoke(touchMovement);
                        }
                        else if (Time.time < tapTimeMax)
                        {
                            OnTap?.Invoke();
                        }
                    }
                }
            }
        }
    }
}
