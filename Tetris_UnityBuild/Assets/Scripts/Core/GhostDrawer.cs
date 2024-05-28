using UnityEngine;

namespace TetrisPuzzle.Core
{
    public class GhostDrawer : MonoBehaviour
    {
        // Variables

        [SerializeField] private Color ghostColor;

        private Shape ghostShape;
        private bool hitBottom;

        private Board gameBoard;


        // Methods

        private void Start()
        {
            gameBoard = FindObjectOfType<Board>();
        }

        public void DrawGhost(Shape originalShape)
        {
            if (ghostShape == null)
            {
                ghostShape = Instantiate(originalShape, originalShape.transform.position, originalShape.transform.rotation);
                ghostShape.gameObject.name = "Ghost Shape";

                foreach (SpriteRenderer spriteRenderer in ghostShape.GetComponentsInChildren<SpriteRenderer>())
                {
                    spriteRenderer.color = ghostColor;
                }
            }
            else
            {
                ghostShape.transform.position = originalShape.transform.position;
                ghostShape.transform.rotation = originalShape.transform.rotation;
            }

            hitBottom = false;
            while (!hitBottom)
            {
                ghostShape.MoveDown();
                if (!gameBoard.IsValidShapePosition(ghostShape))
                {
                    ghostShape.MoveUp();
                    hitBottom = true;
                }
            }
        }

        public void ResetGhostShape()
        {
            Destroy(ghostShape.gameObject);
        }
    }
}
