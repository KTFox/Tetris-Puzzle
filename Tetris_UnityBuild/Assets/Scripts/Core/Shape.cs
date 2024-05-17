using UnityEngine;

namespace TetrisPuzzle.Core
{
    public class Shape : MonoBehaviour
    {
        // Variables

        [SerializeField] private bool canRotate = true;


        // Methods

        private void MoveUp()
        {
            Move(Vector3.up);
        }

        private void MoveDown()
        {
            Move(Vector3.down);
        }

        private void MoveLeft()
        {
            Move(Vector3.left);
        }

        private void MoveRight()
        {
            Move(Vector3.right);
        }

        private void Move(Vector3 moveDirection)
        {
            transform.position += moveDirection;
        }

        private void RotateLeft()
        {
            if (canRotate)
            {
                transform.Rotate(new Vector3(0, 0, 90));
            }
        }

        private void RotateRight()
        {
            if (canRotate)
            {
                transform.Rotate(new Vector3(0, 0, -90));
            }
        }

    }
}
