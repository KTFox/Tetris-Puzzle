using UnityEngine;

namespace TetrisPuzzle.Core
{
    public class Shape : MonoBehaviour
    {
        // Variables

        [SerializeField] private bool canRotate = true;
        [SerializeField] private Vector3 queueOffset;

        // Events

        public Vector3 QueueOffset => queueOffset;


        // Methods

        public void MoveUp()
        {
            Move(Vector3.up);
        }

        public void MoveDown()
        {
            Move(Vector3.down);
        }

        public void MoveLeft()
        {
            Move(Vector3.left);
        }

        public void MoveRight()
        {
            Move(Vector3.right);
        }

        private void Move(Vector3 moveDirection)
        {
            transform.position += moveDirection;
        }

        public void RotateLeft()
        {
            if (canRotate)
            {
                transform.Rotate(new Vector3(0, 0, 90));
            }
        }

        public void RotateRight()
        {
            if (canRotate)
            {
                transform.Rotate(new Vector3(0, 0, -90));
            }
        }

    }
}
