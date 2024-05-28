using TetrisPuzzle.Utilities;
using UnityEngine;

namespace TetrisPuzzle.Core
{
    public class Shape : MonoBehaviour
    {
        // Variables

        [SerializeField] private bool canRotate = true;
        [SerializeField] private Vector3 boxOffset;

        private readonly string GLOW_SQUARE_TAG = "LandShapeFX";
        private GameObject[] glowSquareFX;

        // Properties

        public Vector3 BoxOffset => boxOffset;


        // Methods

        private void Start()
        {
            glowSquareFX = GameObject.FindGameObjectsWithTag(GLOW_SQUARE_TAG);
        }

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

        public void PlayLandingFX()
        {
            int i = 0;
            foreach (Transform child in transform)
            {
                ParticlePlayer particlePlayer = glowSquareFX[i].GetComponent<ParticlePlayer>();
                particlePlayer.transform.position = new Vector3(child.position.x, child.position.y, -2f);
                particlePlayer.Play();
                i++;
            }
        }
    }
}
