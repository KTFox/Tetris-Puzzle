using UnityEngine;

namespace TetrisPuzzle.Core
{
    public class Board : MonoBehaviour
    {
        // Variables

        [SerializeField] private Transform emptySquare;
        [SerializeField] private Vector2Int size = new Vector2Int(10, 30);
        [SerializeField] private int header = 8;

        private Transform[,] grid;


        // Methods

        private void Awake()
        {
            grid = new Transform[size.x, size.y];
        }

        private void Start()
        {
            DrawEmptyCells();
        }

        private void DrawEmptyCells()
        {
            for (int i = 0; i < size.y - header; i++)
            {
                for (int j = 0; j < size.x; j++)
                {
                    Transform cell = Instantiate(emptySquare, new Vector3(j, i, 0), Quaternion.identity);
                    cell.parent = transform;
                    cell.name = $"Board space ({j}, {i})";
                }
            }
        }

        public bool IsValidPosition(Shape shape)
        {
            foreach (Transform child in shape.transform)
            {
                if (!IsWithinBoard(child.position))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsWithinBoard(Vector2 position)
        {
            return position.x >= 0 && position.x < size.x && position.y >= 0;
        }
    }
}
