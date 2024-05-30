using System;
using UnityEngine;

namespace TetrisPuzzle.Managers
{
    public class TouchManager : MonoBehaviour
    {
        // Variables

        [Range(50, 150)]
        [SerializeField] private int MIN_DRAG_DISTANCE = 100;

        [Range(50, 250)]
        [SerializeField] private int MIN_SWIPE_DISTANCE = 200;

        [SerializeField] private float tapTimeWindow = 0.1f;

        private Vector2 touchMovement;
        private float tapTimeMax;

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

                if (touch.phase == TouchPhase.Began)
                {
                    touchMovement = Vector2.zero;
                    tapTimeMax = Time.time + tapTimeWindow;
                }
                else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    touchMovement += touch.deltaPosition;

                    if (touchMovement.magnitude >= MIN_DRAG_DISTANCE)
                    {
                        OnDrag?.Invoke(touchMovement);
                    }
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    if (touchMovement.magnitude >= MIN_SWIPE_DISTANCE)
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
