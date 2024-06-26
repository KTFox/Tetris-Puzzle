using System;
using System.Collections;
using TetrisPuzzle.Utilities;
using Unity.VisualScripting;
using UnityEngine;

namespace TetrisPuzzle.Core
{
    public class Board : MonoBehaviour
    {
        // Variables

        [SerializeField] private Transform emptySquare;
        [SerializeField] private Vector2Int size = new Vector2Int(10, 30);
        [SerializeField] private int header = 8;
        [SerializeField] private ParticlePlayer[] clearRowFXs;

        private Transform[,] grid;

        // Events

        public event Action<int> OnClearRows;


        // Methods

        private void Awake()
        {
            grid = new Transform[size.x, size.y];
        }
        
        public Coroutine DrawEmptyCells()
        {
            return StartCoroutine(DrawEmptyCellsCoroutine());
        }

        private IEnumerator DrawEmptyCellsCoroutine()
        {
            for (int i = 0; i < size.y - header; i++)
            {
                for (int j = 0; j < size.x; j++)
                {
                    Transform cell = Instantiate(emptySquare, new Vector3(j, i, 0), Quaternion.identity);
                    cell.parent = transform;
                    cell.name = $"Board space ({j}, {i})";
                }

                yield return new WaitForSeconds(0.01f);
            }
        }

        public bool IsOccupied(Shape shape)
        {
            foreach (Transform child in shape.transform)
            {
                Vector2Int childPosition = Vector2Int.RoundToInt(child.position);

                try
                {
                    if (grid[childPosition.x, childPosition.y] != null)
                    {
                        return true;
                    }
                }
                catch
                {
                    return true;
                }
            }

            return false;
        }

        public bool HasReachedLeftLimit(Shape shape)
        {
            foreach (Transform child in shape.transform)
            {
                Vector2Int childPosition = Vector2Int.RoundToInt(child.position);

                if (childPosition.x < 0)
                {
                    return true;
                }
            }

            return false;
        }

        public bool HasReachedRightLimit(Shape shape)
        {
            foreach (Transform child in shape.transform)
            {
                Vector2Int childPosition = Vector2Int.RoundToInt(child.position);

                if (childPosition.x >= size.x)
                {
                    return true;
                }
            }

            return false;
        }

        public bool HasReachedBoardRoof(Shape shape)
        {
            foreach (Transform child in shape.transform)
            {
                Vector2Int childPosition = Vector2Int.RoundToInt(child.position);

                if (childPosition.y >= size.y - header)
                {
                    return true;
                }
            }

            return false;
        }

        public bool HasReachedBoardFloor(Shape shape)
        {
            foreach (Transform child in shape.transform)
            {
                Vector2Int childPosition = Vector2Int.RoundToInt(child.position);

                if (childPosition.y < 0)
                {
                    return true;
                }
            }

            return false;
        }

        public void StoreShapeInGrid(Shape shape)
        {
            foreach (Transform child in shape.transform)
            {
                Vector2Int childPos = Vector2Int.RoundToInt(child.position);
                grid[childPos.x, childPos.y] = child;
            }
        }

        public IEnumerator ClearAllCompletedRows()
        {
            int clearedRowAmount = 0;

            for (int i = 0; i < size.y; i++)
            {
                if (IsCompleted(i))
                {
                    PlayClearRowFX(clearedRowAmount, i);
                    clearedRowAmount++;
                }
            }

            yield return new WaitForSeconds(0.5f);

            for (int i = 0; i < size.y; i++)
            {
                if (IsCompleted(i))
                {
                    ClearRow(i);
                    ShiftRowsDown(i + 1);
                    i--;

                    yield return new WaitForSeconds(0.05f);
                }
            }

            OnClearRows?.Invoke(clearedRowAmount);
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

        private void PlayClearRowFX(int effectIndex, int rowIndex)
        {
            clearRowFXs[effectIndex].transform.position = new Vector3(0, rowIndex, -2f);
            clearRowFXs[effectIndex].Play();
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
