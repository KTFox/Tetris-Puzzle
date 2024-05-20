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
                Vector2Int childPosition = Vector2Int.RoundToInt(child.position);

                if (!IsWithinBoard(childPosition))
                {
                    return false;
                }

                if (IsOccupied(childPosition))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsWithinBoard(Vector2Int position)
        {
            return position.x >= 0 && position.x < size.x && position.y >= 0;
        }

        private bool IsOccupied(Vector2Int position)
        {
            // TO-DO: Need add Shape condition?
            return grid[position.x, position.y] != null;
        }

        public void StoreShapeInGrid(Shape shape)
        {
            foreach (Transform child in shape.transform)
            {
                Vector2Int childPos = Vector2Int.RoundToInt(child.position);
                grid[childPos.x, childPos.y] = child;
            }
        }
    }
}
