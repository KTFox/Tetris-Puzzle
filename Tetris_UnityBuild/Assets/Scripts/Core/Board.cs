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

        public void ClearAllCompletedRows()
        {
            for (int i = 0; i < size.y; i++)
            {
                if (IsCompleted(i))
                {
                    ClearRow(i);
                    ShiftRowsDown(i + 1);
                    i--;
                }
            }
        }

        private bool IsCompleted(int rowIndex)
        {
            for (int i = 0; i < size.x; i++)
            {
                if (grid[i, rowIndex] == null)
                {
                    return false;
                }
            }

            return true;
        }

        private void ClearRow(int rowIndex)
        {
            for (int i = 0; i < size.x; i++)
            {
                Destroy(grid[i, rowIndex].gameObject);
                grid[i, rowIndex] = null;
            }
        }

        private void ShiftRowsDown(int startRowIndex)
        {
            for (int i = startRowIndex; i < size.y; i++)
            {
                ShiftOneRowDown(i);
            }
        }

        private void ShiftOneRowDown(int rowIndex)
        {
            for (int i = 0; i < size.x; i++)
            {
                if (grid[i, rowIndex] != null)
                {
                    grid[i, rowIndex].position += Vector3.down;
                    grid[i, rowIndex - 1] = grid[i, rowIndex];
                    grid[i, rowIndex] = null;
                }
            }
        }
    }
}
